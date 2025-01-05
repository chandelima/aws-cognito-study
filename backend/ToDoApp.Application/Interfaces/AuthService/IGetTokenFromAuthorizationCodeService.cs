using ToDoApp.Core.DTOs;
using ToDoApp.Core.Interfaces;
using ToDoApp.Infra.External.DTOs;

namespace ToDoApp.Application.Interfaces.AuthService;
public interface IGetTokenFromAuthorizationCodeService : IDefaultService
{
    public Task<AwsAuthenticationResultDTO?> Get(ValueDTO<string> authorizationCode);
}
