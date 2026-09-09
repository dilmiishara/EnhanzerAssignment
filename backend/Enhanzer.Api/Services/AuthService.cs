using System.Net.Http.Json;
using System.Text.Json;
using Enhanzer.Api.DTOs;
using Enhanzer.Api.Models;

namespace Enhanzer.Api.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly LocationService _locationService;
    private readonly JwtService _jwtService;

    private const string LoginApiUrl =
        "https://ez-staging-api.azurewebsites.net/api/External_Api/POS_Api/Invoke";

    public AuthService(
        HttpClient httpClient,
        LocationService locationService,
        JwtService jwtService)
    {
        _httpClient = httpClient;
        _locationService = locationService;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> LoginAsync(
        LoginRequest request)
    {
        var externalRequest =
            new ExternalLoginRequest
            {
                CompanyCode = request.Email,

                ApiBody = new ExternalLoginBody
                {
                    Username = request.Email,
                    Password = request.Password
                }
            };

        var response =
            await _httpClient.PostAsJsonAsync(
                LoginApiUrl,
                externalRequest
            );

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Authentication service returned HTTP status {(int)response.StatusCode}."
            );
        }

        var responseContent =
            await response.Content.ReadAsStringAsync();

        Console.WriteLine("========== EXTERNAL LOGIN RESPONSE ==========");
        Console.WriteLine(responseContent);
        Console.WriteLine("=============================================");

        var loginResponse =
            JsonSerializer.Deserialize<ExternalLoginResponse>(
                responseContent
            );

        if (loginResponse is null)
        {
            throw new JsonException(
                "Authentication service returned an empty response."
            );
        }

        if (loginResponse.StatusCode != 200 ||
            loginResponse.ResponseBody is null ||
            loginResponse.ResponseBody.Count == 0)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password."
            );
        }

        var user = loginResponse.ResponseBody[0];

        var locationsProcessed =
            await _locationService
                .SaveOrUpdateLocationsAsync(
                    user.UserLocations
                );

        var token = _jwtService.GenerateToken(
            user.UserCode,
            user.Email,
            user.UserDisplayName
        );

        return new LoginResponse
        {
            Success = true,
            Message = "Login successful.",
            DisplayName = user.UserDisplayName,
            Email = user.Email,
            LocationsProcessed = locationsProcessed,
            Token = token
        };
    }
}