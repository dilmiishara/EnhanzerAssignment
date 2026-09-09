namespace Enhanzer.Api.DTOs;

public class LoginResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int LocationsProcessed { get; set; }
    
    public string Token { get; set; } = string.Empty;

}