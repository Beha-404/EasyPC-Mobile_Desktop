using EasyPC.Model;

namespace EasyPC.Services.Interfaces;

public interface IWishlistService
{
    Task<List<Wishlist>> GetByUserIdAsync(int userId);
    Task<Wishlist?> AddToWishlistAsync(int userId, int pcId);
    Task<bool> RemoveFromWishlistAsync(int userId, int pcId);
    Task<bool> IsInWishlistAsync(int userId, int pcId);
}
