using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPC.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductsService service) : ControllerBase
{
    protected IProductsService _service = service;

    [HttpGet("get/all")]
    public async Task<Model.Products> GetAll() => await _service.GetAllProducts();
}
