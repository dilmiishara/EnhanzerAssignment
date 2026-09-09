using System.Text.Json;
using Enhanzer.Api.DTOs;
using Enhanzer.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enhanzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // Simple test endpoint
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new
        {
            message = "Auth API is working"
        });
    }

    // Login endpoint
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var result =
                await _authService.LoginAsync(request);

            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new
            {
                success = false,
                message = "Invalid email or password."
            });
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new
            {
                success = false,
                message =
                    "Unable to communicate with the authentication service.",
                error = ex.Message
            });
        }
        catch (JsonException)
        {
            return StatusCode(502, new
            {
                success = false,
                message =
                    "The authentication service returned an invalid response."
            });
        }
    }

    // Protected endpoint
    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(new
        {
            message = "You are authenticated.",

            userCode = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )?.Value,

            email = User.FindFirst(
                System.Security.Claims.ClaimTypes.Email
            )?.Value,

            displayName = User.Identity?.Name
        });
    }
}