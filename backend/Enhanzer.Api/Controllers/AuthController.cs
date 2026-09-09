using Enhanzer.Api.DTOs;
using Enhanzer.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Enhanzer.Api.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new
        {
            message = "Auth API is working"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var result =
                await _authService.LoginAsync(request);

            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new
            {
                success = false,
                message = "Unable to communicate with the authentication service.",
                error = ex.Message
            });
        }
        catch (JsonException)
        {
            return StatusCode(502, new
            {
                success = false,
                message = "The authentication service returned an invalid response."
            });
        }
    }
}