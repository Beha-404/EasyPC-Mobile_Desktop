using EasyPC.Services.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EasyPC.API.Hubs;

[Authorize]
public class SupportHub(DatabaseContext context, ILogger<SupportHub> logger) : Hub
{
    private readonly DatabaseContext _context = context;
    private readonly ILogger<SupportHub> _logger = logger;

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = Context.User?.Identity?.Name;

        if (userId != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
            _logger.LogInformation($"User {username} (ID: {userId}) connected to Support Hub");
        }

        var user = await _context.Users.FindAsync(int.Parse(userId ?? "0"));
        if (user!.Role == UserRole.Admin || user.Role == UserRole.Manager || user.Role == UserRole.SuperAdmin)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "admins");
            _logger.LogInformation($"{user.Role} {username} connected to Support Hub");
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = Context.User?.Identity?.Name;

        _logger.LogInformation($"User {username} (ID: {userId}) disconnected from Support Hub");

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string message, int? targetUserId = null)
    {
        var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = Context.User?.Identity?.Name;

        if (string.IsNullOrWhiteSpace(userIdClaim) || string.IsNullOrWhiteSpace(username))
        {
            throw new HubException("User not authenticated");
        }

        var userId = int.Parse(userIdClaim);

        if (targetUserId == 0)
        {
            targetUserId = null;
        }

        var user = await _context.Users.FindAsync(userId);
        var isAdmin = user?.Role == UserRole.Admin || user?.Role == UserRole.Manager || user?.Role == UserRole.SuperAdmin;

        var conversationUserId = isAdmin && targetUserId.HasValue ? targetUserId.Value : userId;

        var supportMessage = new SupportMessage
        {
            SenderId = userId,
            ConversationUserId = conversationUserId,
            SenderName = username,
            Message = message,
            IsAdmin = isAdmin,
            Timestamp = DateTime.UtcNow,
            IsRead = false
        };

        _context.SupportMessages.Add(supportMessage);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Message from {username} (Admin: {isAdmin}) to conversation {conversationUserId}: {message}");

        var messagePayload = new
        {
            supportMessage.Id,
            supportMessage.SenderId,
            ConversationUserId = conversationUserId,
            supportMessage.SenderName,
            supportMessage.Message,
            supportMessage.IsAdmin,
            supportMessage.Timestamp,
            supportMessage.IsRead
        };

        if (isAdmin && targetUserId.HasValue)
        {
            await Clients.Group($"user_{targetUserId}").SendAsync("ReceiveMessage", messagePayload);

            await Clients.GroupExcept("admins", Context.ConnectionId).SendAsync("ReceiveMessage", messagePayload);

            await Clients.Caller.SendAsync("ReceiveMessage", messagePayload);
        }
        else
        {
            await Clients.Group("admins").SendAsync("ReceiveMessage", messagePayload);

            await Clients.Caller.SendAsync("ReceiveMessage", messagePayload);
        }
    }

    public async Task<List<object>> GetMessageHistory()
    {
        var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
        {
            throw new HubException("User not authenticated");
        }

        var userId = int.Parse(userIdClaim);
        var user = await _context.Users.FindAsync(userId);
        var isAdmin = user?.Role == UserRole.Admin || user?.Role == UserRole.Manager || user?.Role == UserRole.SuperAdmin;

        List<SupportMessage> messages;

        if (isAdmin)
        {
            messages = await _context.SupportMessages
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
        else
        {
            messages = await _context.SupportMessages
                .Where(m => m.ConversationUserId == userId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        return messages.Select(m => new
        {
            m.Id,
            m.SenderId,
            m.ConversationUserId,
            m.SenderName,
            m.Message,
            m.IsAdmin,
            m.Timestamp,
            m.IsRead
        }).Cast<object>().ToList();
    }

    public async Task<List<object>> GetAllConversations()
    {
        var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
        {
            throw new HubException("User not authenticated");
        }

        var userId = int.Parse(userIdClaim);
        var user = await _context.Users.FindAsync(userId);

        _logger.LogInformation($"GetAllConversations: User {user?.Username} (ID: {userId}) has role: {user?.Role}");

        if (user?.Role != UserRole.Admin && user?.Role != UserRole.Manager && user?.Role != UserRole.SuperAdmin)
        {
            throw new HubException($"Only admins, managers, and superadmins can access all conversations. Current user role: {user?.Role}");
        }

        var nonAdminUserIds = await _context.SupportMessages
            .Where(m => !m.IsAdmin)
            .Select(m => m.ConversationUserId)
            .Distinct()
            .ToListAsync();

        var conversations = new List<object>();

        foreach (var nonAdminUserId in nonAdminUserIds)
        {
            var userMessages = await _context.SupportMessages
                .Where(m => m.ConversationUserId == nonAdminUserId)
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();

            if (userMessages.Any())
            {
                var lastMessage = userMessages.First();
                var nonAdminUser = await _context.Users.FindAsync(nonAdminUserId);

                conversations.Add(new
                {
                    UserId = nonAdminUserId,
                    Username = nonAdminUser?.Username ?? lastMessage.SenderName,
                    LastMessage = lastMessage.Message,
                    LastMessageTime = lastMessage.Timestamp,
                    UnreadCount = userMessages.Count(m => !m.IsRead && !m.IsAdmin)
                });
            }
        }

        return conversations.OrderByDescending(c => ((dynamic)c).LastMessageTime).ToList();
    }

    public async Task MarkAsRead(int messageId)
    {
        var message = await _context.SupportMessages.FindAsync(messageId);
        if (message != null)
        {
            message.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task MarkConversationAsRead(int conversationUserId)
    {
        var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userIdClaim))
        {
            throw new HubException("User not authenticated");
        }

        var userId = int.Parse(userIdClaim);
        var user = await _context.Users.FindAsync(userId);

        if (user?.Role != UserRole.Admin && user?.Role != UserRole.Manager && user?.Role != UserRole.SuperAdmin)
        {
            return;
        }

        var unreadMessages = await _context.SupportMessages
            .Where(m => m.ConversationUserId == conversationUserId && !m.IsRead && !m.IsAdmin)
            .ToListAsync();

        foreach (var message in unreadMessages)
        {
            message.IsRead = true;
        }

        await _context.SaveChangesAsync();
    }
}
