using System.Text.Json.Serialization;

namespace ToDoApp.Infra.External.DTOs;

public class AwsAuthenticationResultDTO
{
    public string? TokenType { get; set; }
    public string? IdToken { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public int? ExpiresIn { get; set; }
}
