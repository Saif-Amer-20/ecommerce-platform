using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var (success, token, user) = await _userService.RegisterAsync(
            request.Email, 
            request.Password, 
            request.FullName, 
            request.Phone);

        if (success)
        {
            return Ok(new 
            { 
                message = "تم إنشاء الحساب بنجاح",
                accessToken = token,
                user = new 
                {
                    id = user?.Id,
                    email = user?.Email,
                    fullName = user?.FullName
                }
            });
        }

        return BadRequest(new { message = "فشل في إنشاء الحساب" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var (success, token, user) = await _userService.LoginAsync(request.Email, request.Password);

        if (success)
        {
            return Ok(new 
            { 
                message = "تم تسجيل الدخول بنجاح",
                accessToken = token,
                user = new 
                {
                    id = user?.Id,
                    email = user?.Email,
                    fullName = user?.FullName
                }
            });
        }

        return Unauthorized(new { message = "بيانات الدخول غير صحيحة" });
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        // In a real implementation, get user ID from JWT token
        var userId = 1;
        var user = await _userService.GetUserProfileAsync(userId);
        
        if (user != null)
        {
            return Ok(new 
            {
                id = user.Id,
                email = user.Email,
                fullName = user.FullName,
                phone = user.Phone,
                isActive = user.IsActive
            });
        }

        return NotFound(new { message = "المستخدم غير موجود" });
    }
}

public record RegisterRequest(string Email, string Password, string FullName, string? Phone);
public record LoginRequest(string Email, string Password);