using ToDoApp.Core.DTOs;
using ToDoApp.Core.Interfaces;
using ToDoApp.Infra.External.DTOs;

namespace ToDoApp.Application.Interfaces.AuthService;
public interface IRefreshTokenService : IDefaultService
{
    public Task<AwsAuthenticationResultDTO?> Refresh(ValueDTO<string> refreshToken);
}
