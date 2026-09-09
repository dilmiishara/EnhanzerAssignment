using System.Net.Http.Json;
using System.Text.Json;
using Enhanzer.Api.DTOs;
using Enhanzer.Api.Models;

namespace Enhanzer.Api.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    private const string LoginApiUrl =
        "https://ez-staging-api.azurewebsites.net/api/External_Api/POS_Api/Invoke";

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JsonElement> LoginAsync(LoginRequest request)
    {
        var externalRequest = new ExternalLoginRequest
        {
            CompanyCode = request.Email,

            ApiBody = new ExternalLoginBody
            {
                Username = request.Email,
                Password = request.Password
            }
        };

        var response = await _httpClient.PostAsJsonAsync(
            LoginApiUrl,
            externalRequest
        );

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"External login API returned status code {(int)response.StatusCode}."
            );
        }

        using var document =
            JsonDocument.Parse(responseContent);

        return document.RootElement.Clone();
    }
}