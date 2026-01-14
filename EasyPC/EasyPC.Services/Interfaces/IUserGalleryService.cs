namespace EasyPC.Services.Interfaces;

public interface IUserGalleryService
{
    Task<Model.UserGallery> AddImageAsync(int orderId, int userId, byte[] imageData, string? description);
    Task<List<Model.UserGallery>> GetAllImagesAsync();
    Task<List<Model.UserGallery>> GetUserImagesAsync(int userId);
    Task<bool> DeleteImageAsync(int id, int userId);
}
