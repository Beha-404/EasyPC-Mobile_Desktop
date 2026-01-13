using EasyPC.Model;
using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPC.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<List<Wishlist>>> GetUserWishlist(int userId)
    {
        var wishlist = await _wishlistService.GetByUserIdAsync(userId);
        return Ok(wishlist);
    }

    [HttpPost]
    public async Task<ActionResult<Wishlist>> AddToWishlist([FromBody] WishlistRequest request)
    {
        var result = await _wishlistService.AddToWishlistAsync(request.UserId, request.PcId);
        
        if (result == null)
        {
            return BadRequest("PC is already in wishlist or does not exist");
        }

        return Ok(result);
    }

    [HttpDelete("{userId}/{pcId}")]
    public async Task<ActionResult> RemoveFromWishlist(int userId, int pcId)
    {
        var result = await _wishlistService.RemoveFromWishlistAsync(userId, pcId);
        
        if (!result)
        {
            return NotFound("Item not found in wishlist");
        }

        return Ok(new { message = "Removed from wishlist" });
    }

    [HttpGet("{userId}/check/{pcId}")]
    public async Task<ActionResult<bool>> IsInWishlist(int userId, int pcId)
    {
        var result = await _wishlistService.IsInWishlistAsync(userId, pcId);
        return Ok(result);
    }
}

public class WishlistRequest
{
    public int UserId { get; set; }
    public int PcId { get; set; }
}
