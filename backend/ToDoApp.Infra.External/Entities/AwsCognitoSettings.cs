namespace ToDoApp.Infra.External.Entities;
public class AwsCognitoSettings
{
    public string Domain { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string LoginPageUrl { get; set; } = null!;
    public string LoginPageRedirectUri { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
}
