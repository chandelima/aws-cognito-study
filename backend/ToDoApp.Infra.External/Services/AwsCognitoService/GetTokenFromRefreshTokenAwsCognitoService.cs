using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Services;
using ToDoApp.Infra.External.Constants;
using ToDoApp.Infra.External.DTOs;
using ToDoApp.Infra.External.Entities;
using ToDoApp.Infra.External.Interfaces;

namespace ToDoApp.Infra.External.Services.AwsCognitoService;
internal class GetTokenFromRefreshTokenAwsCognitoService : DefaultService, IGetTokenFromRefreshTokenAwsCognitoService
{
    private readonly AwsCognitoSettings _awsCognitoSettings;
    private readonly IHttpClientFactory _httpClientFactory;

    public GetTokenFromRefreshTokenAwsCognitoService(
        IOptions<AwsCognitoSettings> awsCognitoSettings, 
        IHttpClientFactory httpClientFactory)
    {
        _awsCognitoSettings = awsCognitoSettings.Value;
        _httpClientFactory = httpClientFactory;
    }

    private FormUrlEncodedContent BuildRequestContent(string refreshToken)
    {
        var requestData = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "client_id", _awsCognitoSettings.ClientId },
            { "client_secret", _awsCognitoSettings.ClientSecret },
            { "refresh_token", refreshToken },
        };

        var content = new FormUrlEncodedContent(requestData);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        return content;
    }

    private AwsAuthenticationResultDTO? GetDTO(Dictionary<string, string>? response)
    {
        if (response == null) return null;

        var dto = new AwsAuthenticationResultDTO();

        dto.TokenType = response["token_type"];
        dto.IdToken = response["id_token"];
        dto.AccessToken = response["access_token"];
        
        if (int.TryParse(response["expires_in"], out var value))
        {
            dto.ExpiresIn = value;
        }

        return dto;
    }

    public async Task<AwsAuthenticationResultDTO?> RefreshToken(string refreshToken)
    {
        var client = _httpClientFactory.CreateClient(ConfigurationsConstants.AWS_COGNITO_DOMAIN_KEY);
        var url = $"{_awsCognitoSettings.Domain}/oauth2/token";

        var content = BuildRequestContent(refreshToken);
        HttpResponseMessage response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

            return GetDTO(tokens);
        }
        else
        {
            Messages.AddError("Erro na obtenção de token.");
        }

        return null;
    }
}
