using System.Text.Json.Serialization;

namespace Enhanzer.Api.Models;

public class ExternalLoginResponse
{
    [JsonPropertyName("Status_Code")]
    public int StatusCode { get; set; }

    [JsonPropertyName("Sync_Time")]
    public string? SyncTime { get; set; }

    [JsonPropertyName("Message")]
    public string? Message { get; set; }

    [JsonPropertyName("Response_Body")]
    public List<ExternalLoginUser>? ResponseBody { get; set; }
}

public class ExternalLoginUser
{
    [JsonPropertyName("User_Code")]
    public string UserCode { get; set; } = string.Empty;

    [JsonPropertyName("User_Display_Name")]
    public string UserDisplayName { get; set; } = string.Empty;

    [JsonPropertyName("Email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("Company_Code")]
    public string CompanyCode { get; set; } = string.Empty;

    [JsonPropertyName("User_Locations")]
    public List<ExternalLocation> UserLocations { get; set; } = new();
}

public class ExternalLocation
{
    [JsonPropertyName("Location_Code")]
    public string LocationCode { get; set; } = string.Empty;

    [JsonPropertyName("Location_Name")]
    public string LocationName { get; set; } = string.Empty;
}