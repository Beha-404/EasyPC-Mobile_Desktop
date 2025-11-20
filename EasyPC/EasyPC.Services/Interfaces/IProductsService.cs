using EasyPC.Model.Requests.RamRequests;
using EasyPC.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.Interfaces
{
    public interface IProductsService
    {
       Task<Model.Products> GetAllProducts();
    }
}
