using ToDoApp.Application.Interfaces.AuthService;
using ToDoApp.Core.DTOs;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Services;
using ToDoApp.Infra.External.Interfaces;

namespace ToDoApp.Application.Services.AuthService;
internal class RevokeTokenService : DefaultService, IRevokeTokenService
{
    private readonly IRevokeTokenAwsCognitoService _revokeTokenFromRefreshTokenAwsCognitoService;

    public RevokeTokenService(IRevokeTokenAwsCognitoService revokeTokenFromRefreshTokenAwsCognitoService)
    {
        _revokeTokenFromRefreshTokenAwsCognitoService = revokeTokenFromRefreshTokenAwsCognitoService;
    }

    private bool Validate(string? refreshToken)
    {
        Messages.AddErrorIf(
            string.IsNullOrWhiteSpace(refreshToken),
            "Refresh token deve ser informado.");

        return Messages.Count() == 0;
    }

    public async Task Revoke(ValueDTO<string> dto)
    {
        var refreshToken = dto.Value;

        if (!Validate(refreshToken))
        {
            return;
        }

        await _revokeTokenFromRefreshTokenAwsCognitoService.Revoke(refreshToken!);
        Messages.AddRange(_revokeTokenFromRefreshTokenAwsCognitoService.Messages);
    }
}
