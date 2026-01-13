using System.ComponentModel.DataAnnotations;

namespace EasyPC.Services.Database;

public class SupportMessage
{
    [Key]
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int ConversationUserId { get; set; }

    public string? SenderName { get; set; }

    public string? Message { get; set; }

    public bool IsAdmin { get; set; }

    public DateTime Timestamp { get; set; }

    public bool IsRead { get; set; }

    public virtual User? User { get; set; }
}
