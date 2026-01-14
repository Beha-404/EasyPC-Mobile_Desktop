using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPC.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserGalleryController(IUserGalleryService service) : ControllerBase
{
    private readonly IUserGalleryService _service = service;

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] int orderId, [FromForm] IFormFile image, [FromForm] string? description)
    {
        try
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            if (image == null || image.Length == 0)
            {
                return BadRequest("Image is required.");
            }

            // Validate image type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Only JPG, JPEG, and PNG images are allowed.");
            }

            // Validate image size (max 5MB)
            if (image.Length > 5 * 1024 * 1024)
            {
                return BadRequest("Image size must be less than 5MB.");
            }

            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            var result = await _service.AddImageAsync(orderId, userId, imageData, description);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllImages()
    {
        try
        {
            var images = await _service.GetAllImagesAsync();
            return Ok(images);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserImages(int userId)
    {
        try
        {
            var images = await _service.GetUserImagesAsync(userId);
            return Ok(images);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImage(int id)
    {
        try
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _service.DeleteImageAsync(id, userId);
            if (!result)
            {
                return NotFound("Image not found or you don't have permission to delete it.");
            }

            return Ok("Image deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [AllowAnonymous]
    [HttpGet("image/{imageName}")]
    public IActionResult GetImage(string imageName)
    {
        try
        {
            var imagePath = Path.Combine("Assets/Images/UserGallery", imageName);
            
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("Image not found.");
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/jpeg");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
