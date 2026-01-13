using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelWishlist = EasyPC.Model.Wishlist;
using ModelPC = EasyPC.Model.PC;
using ModelPcType = EasyPC.Model.PcType;

namespace EasyPC.Services;

public class WishlistService : IWishlistService
{
    private readonly DatabaseContext _context;

    public WishlistService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<ModelWishlist>> GetByUserIdAsync(int userId)
    {
        var wishlists = await _context.Wishlists
            .Where(w => w.UserId == userId)
            .Include(w => w.PC)
                .ThenInclude(p => p!.PcType)
            .OrderByDescending(w => w.DateAdded)
            .ToListAsync();

        return wishlists.Select(w => new ModelWishlist
        {
            Id = w.Id,
            UserId = w.UserId,
            PcId = w.PcId,
            DateAdded = w.DateAdded,
            PC = w.PC != null ? new ModelPC
            {
                Id = w.PC.Id,
                Name = w.PC.Name,
                Price = w.PC.Price,
                Picture = w.PC.Picture,
                Available = w.PC.Available,
                AverageRating = w.PC.AverageRating != null ? (int)w.PC.AverageRating : null,
                RatingCount = w.PC.RatingCount,
                PcType = w.PC.PcType != null ? new ModelPcType { Id = w.PC.PcType.Id, Name = w.PC.PcType.Name } : null,
            } : null
        }).ToList();
    }

    public async Task<ModelWishlist?> AddToWishlistAsync(int userId, int pcId)
    {
        // Check if already in wishlist
        var existing = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.UserId == userId && w.PcId == pcId);

        if (existing != null)
        {
            return null; // Already in wishlist
        }

        // Check if PC exists
        var pc = await _context.PCs.FindAsync(pcId);
        if (pc == null)
        {
            return null;
        }

        var wishlistItem = new Wishlist
        {
            UserId = userId,
            PcId = pcId,
            DateAdded = DateTime.UtcNow
        };

        _context.Wishlists.Add(wishlistItem);
        await _context.SaveChangesAsync();

        return new ModelWishlist
        {
            Id = wishlistItem.Id,
            UserId = wishlistItem.UserId,
            PcId = wishlistItem.PcId,
            DateAdded = wishlistItem.DateAdded
        };
    }

    public async Task<bool> RemoveFromWishlistAsync(int userId, int pcId)
    {
        var wishlistItem = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.UserId == userId && w.PcId == pcId);

        if (wishlistItem == null)
        {
            return false;
        }

        _context.Wishlists.Remove(wishlistItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsInWishlistAsync(int userId, int pcId)
    {
        return await _context.Wishlists
            .AnyAsync(w => w.UserId == userId && w.PcId == pcId);
    }
}
