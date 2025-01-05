using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Services;
using ToDoApp.Infra.External.Constants;
using ToDoApp.Infra.External.DTOs;
using ToDoApp.Infra.External.Entities;
using ToDoApp.Infra.External.Interfaces;

namespace ToDoApp.Infra.External.Services.AwsCognitoService;
internal class RevokeTokenAwsCognitoService : DefaultService, IRevokeTokenAwsCognitoService
{
    private readonly AwsCognitoSettings _awsCognitoSettings;
    private readonly IHttpClientFactory _httpClientFactory;

    public RevokeTokenAwsCognitoService(
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
            { "token", refreshToken }
        };

        var content = new FormUrlEncodedContent(requestData);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        return content;
    }

    public async Task Revoke(string refreshToken)
    {
        var client = _httpClientFactory.CreateClient(ConfigurationsConstants.AWS_COGNITO_DOMAIN_KEY);

        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_awsCognitoSettings.ClientId}:{_awsCognitoSettings.ClientSecret}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        
        var content = BuildRequestContent(refreshToken);

        var url = $"{_awsCognitoSettings.Domain}/oauth2/revoke";
        HttpResponseMessage response = await client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            Messages.AddError("Erro na revogação de token.");
        }
    }
}
