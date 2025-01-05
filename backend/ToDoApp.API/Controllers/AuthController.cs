using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ToDoApp.API.Config;
using ToDoApp.API.Entities;
using ToDoApp.Application.Interfaces.AuthService;
using ToDoApp.Core.DTOs;
using ToDoApp.Infra.External.Entities;

namespace ToDoApp.API.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IGetTokenFromAuthorizationCodeService _getTokenFromAuthorizationCodeService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IRevokeTokenService _revokeTokenService;
    private readonly AwsCognitoSettings _awsCognitoSettings;

    public AuthController(
        IGetTokenFromAuthorizationCodeService getTokenFromAuthorizationCodeService,
        IRefreshTokenService refreshTokenService,
        IOptions<AwsCognitoSettings> awsCognitoSettings,
        IRevokeTokenService revokeTokenService)
    {
        _getTokenFromAuthorizationCodeService = getTokenFromAuthorizationCodeService;
        _refreshTokenService = refreshTokenService;
        _awsCognitoSettings = awsCognitoSettings.Value;
        _revokeTokenService = revokeTokenService;
    }

    [HttpGet("get_login_url")]
    public IActionResult GetLoginUrl()
    {
        var response = new DafaultResponse(
            new ValueDTO<string>(_awsCognitoSettings.LoginPageUrl),
            []);

        return this.ProcessResponse(response);
    }

    [HttpPost("get_token_from_auth_code")]
    public async Task<IActionResult> GetToken(ValueDTO<string> authorizationCode)
    {
        var response = new DafaultResponse(
            await _getTokenFromAuthorizationCodeService.Get(authorizationCode),
            _getTokenFromAuthorizationCodeService.Messages);

        return this.ProcessResponse(response);
    }

    [HttpPost("refresh_token")]
    public async Task<IActionResult> RefreshToken(ValueDTO<string> refreshToken)
    {
        var response = new DafaultResponse(
            await _refreshTokenService.Refresh(refreshToken),
            _refreshTokenService.Messages);

        return this.ProcessResponse(response);
    }

    [HttpPost("revoke_token")]
    public async Task<IActionResult> RevokeToken(ValueDTO<string> refreshToken)
    {
        await _revokeTokenService.Revoke(refreshToken);

        var response = new DafaultResponse(
            new {},
            _revokeTokenService.Messages);

        return this.ProcessResponse(response);
    }
}
