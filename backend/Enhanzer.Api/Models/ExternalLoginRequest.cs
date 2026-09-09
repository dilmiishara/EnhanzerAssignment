using System.Text.Json.Serialization;

namespace Enhanzer.Api.Models;

public class ExternalLoginRequest
{
    [JsonPropertyName("API_Action")]
    public string ApiAction { get; set; } = "GetLoginData";

    [JsonPropertyName("Device_Id")]
    public string DeviceId { get; set; } = "D001";

    [JsonPropertyName("Sync_Time")]
    public string SyncTime { get; set; } = string.Empty;

    [JsonPropertyName("Company_Code")]
    public string CompanyCode { get; set; } = string.Empty;

    [JsonPropertyName("API_Body")]
    public ExternalLoginBody ApiBody { get; set; } = new();
}

public class ExternalLoginBody
{
    [JsonPropertyName("Username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("Pw")]
    public string Password { get; set; } = string.Empty;
}