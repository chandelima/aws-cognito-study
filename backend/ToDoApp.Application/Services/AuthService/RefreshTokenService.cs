using ToDoApp.Application.Interfaces.AuthService;
using ToDoApp.Core.DTOs;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Services;
using ToDoApp.Infra.External.DTOs;
using ToDoApp.Infra.External.Interfaces;

namespace ToDoApp.Application.Services.AuthService;
internal class RefreshTokenService : DefaultService, IRefreshTokenService
{
    private readonly IGetTokenFromRefreshTokenAwsCognitoService _getTokenFromRefreshTokenAwsCognitoService;

    public RefreshTokenService(IGetTokenFromRefreshTokenAwsCognitoService getTokenFromRefreshTokenAwsCognitoService)
    {
        _getTokenFromRefreshTokenAwsCognitoService = getTokenFromRefreshTokenAwsCognitoService;
    }

    private bool Validate(string? refreshToken)
    {
        Messages.AddErrorIf(
            string.IsNullOrWhiteSpace(refreshToken),
            "Refresh token deve ser informado.");

        return Messages.Count() == 0;
    }

    public async Task<AwsAuthenticationResultDTO?> Refresh(ValueDTO<string> dto)
    {
        var refreshToken = dto.Value;

        if (!Validate(refreshToken))
        {
            return null;
        }

        var result = await _getTokenFromRefreshTokenAwsCognitoService.RefreshToken(refreshToken!);
        Messages.AddRange(_getTokenFromRefreshTokenAwsCognitoService.Messages);

        return result;
    }
}
