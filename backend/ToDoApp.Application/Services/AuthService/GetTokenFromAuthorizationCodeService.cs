using System.Data;
using ToDoApp.Application.Interfaces.AuthService;
using ToDoApp.Core.DTOs;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Services;
using ToDoApp.Infra.External.DTOs;
using ToDoApp.Infra.External.Interfaces;

namespace ToDoApp.Application.Services.AuthService;
internal class GetTokenFromAuthorizationCodeService : DefaultService, IGetTokenFromAuthorizationCodeService
{
    private readonly IGetTokenFromAuthCodeAwsCognitoService _tokenFromAuthCodeAwsCognitoService;

    public GetTokenFromAuthorizationCodeService(
        IGetTokenFromAuthCodeAwsCognitoService tokenFromAuthCodeAwsCognitoService)
    {
        _tokenFromAuthCodeAwsCognitoService = tokenFromAuthCodeAwsCognitoService;
    }

    private bool Validate(string? authorizationCode)
    {
        Messages.AddErrorIf(
            string.IsNullOrWhiteSpace(authorizationCode), 
            "Código de autorização deve ser informado.");

        return Messages.Count() == 0;
    }

    public async Task<AwsAuthenticationResultDTO?> Get(ValueDTO<string> dto)
    {
        var authorizationCode = dto.Value;


        if (!Validate(authorizationCode))
        {
            return null;
        }
        
        var loginResult = await _tokenFromAuthCodeAwsCognitoService.Get(authorizationCode!);
        Messages.AddRange(_tokenFromAuthCodeAwsCognitoService.Messages);

        return loginResult;
    }
}
