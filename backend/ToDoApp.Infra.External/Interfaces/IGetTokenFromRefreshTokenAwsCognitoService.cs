using ToDoApp.Core.Interfaces;
using ToDoApp.Infra.External.DTOs;

namespace ToDoApp.Infra.External.Interfaces;
public interface IGetTokenFromRefreshTokenAwsCognitoService : IDefaultService
{
    public Task<AwsAuthenticationResultDTO?> RefreshToken(string refreshToken);
}
