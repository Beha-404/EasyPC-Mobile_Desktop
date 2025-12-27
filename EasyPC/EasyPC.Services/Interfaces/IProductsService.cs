using EasyPC.Model;

namespace EasyPC.Services.Interfaces;

public interface IProductsService
{
    Task<Products> GetAllProducts();
}
