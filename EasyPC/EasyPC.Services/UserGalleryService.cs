using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyPC.Services;

public class UserGalleryService(DatabaseContext context) : IUserGalleryService
{
    private readonly DatabaseContext _context = context;
    private const string ImageDirectory = "Assets/Images/UserGallery";

    public async Task<Model.UserGallery> AddImageAsync(int orderId, int userId, byte[] imageData, string? description)
    {
        // Verify that the order belongs to the user
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

        if (order == null)
        {
            throw new UnauthorizedAccessException("Order not found or doesn't belong to the user.");
        }

        // Check if image already exists for this order
        var existingImage = await _context.UserGalleries
            .FirstOrDefaultAsync(ug => ug.OrderId == orderId);

        if (existingImage != null)
        {
            throw new InvalidOperationException("An image for this order already exists.");
        }

        // Create directory if it doesn't exist
        if (!Directory.Exists(ImageDirectory))
        {
            Directory.CreateDirectory(ImageDirectory);
        }

        // Save image to disk
        var fileName = $"{Guid.NewGuid()}.jpg";
        var filePath = Path.Combine(ImageDirectory, fileName);
        await File.WriteAllBytesAsync(filePath, imageData);

        // Create database entry
        var userGallery = new Database.UserGallery
        {
            UserId = userId,
            OrderId = orderId,
            ImagePath = filePath,
            Description = description,
            UploadDate = DateTime.UtcNow
        };

        _context.UserGalleries.Add(userGallery);
        await _context.SaveChangesAsync();

        // Map to model
        return new Model.UserGallery
        {
            Id = userGallery.Id,
            UserId = userGallery.UserId,
            OrderId = userGallery.OrderId,
            ImagePath = userGallery.ImagePath,
            UploadDate = userGallery.UploadDate,
            Description = userGallery.Description
        };
    }

    public async Task<List<Model.UserGallery>> GetAllImagesAsync()
    {
        var images = await _context.UserGalleries
            .Include(ug => ug.User)
            .Include(ug => ug.Order)
            .OrderByDescending(ug => ug.UploadDate)
            .ToListAsync();

        return images.Select(ug => new Model.UserGallery
        {
            Id = ug.Id,
            UserId = ug.UserId,
            OrderId = ug.OrderId,
            ImagePath = ug.ImagePath,
            UploadDate = ug.UploadDate,
            Description = ug.Description,
            User = ug.User != null ? new Model.User
            {
                Id = ug.User.Id,
                Username = ug.User.Username,
                FirstName = ug.User.FirstName,
                LastName = ug.User.LastName
            } : null
        }).ToList();
    }

    public async Task<List<Model.UserGallery>> GetUserImagesAsync(int userId)
    {
        var images = await _context.UserGalleries
            .Include(ug => ug.User)
            .Include(ug => ug.Order)
            .Where(ug => ug.UserId == userId)
            .OrderByDescending(ug => ug.UploadDate)
            .ToListAsync();

        return images.Select(ug => new Model.UserGallery
        {
            Id = ug.Id,
            UserId = ug.UserId,
            OrderId = ug.OrderId,
            ImagePath = ug.ImagePath,
            UploadDate = ug.UploadDate,
            Description = ug.Description,
            User = ug.User != null ? new Model.User
            {
                Id = ug.User.Id,
                Username = ug.User.Username,
                FirstName = ug.User.FirstName,
                LastName = ug.User.LastName
            } : null
        }).ToList();
    }

    public async Task<bool> DeleteImageAsync(int id, int userId)
    {
        var image = await _context.UserGalleries
            .FirstOrDefaultAsync(ug => ug.Id == id && ug.UserId == userId);

        if (image == null)
        {
            return false;
        }

        // Delete file from disk
        if (File.Exists(image.ImagePath))
        {
            File.Delete(image.ImagePath);
        }

        _context.UserGalleries.Remove(image);
        await _context.SaveChangesAsync();

        return true;
    }
}
