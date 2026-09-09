using Enhanzer.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Enhanzer.Api.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new
        {
            message = "Auth API is working"
        });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        return Ok(new
        {
            success = true,
            message = "Login request received successfully",
            email = request.Email
        });
    }
}