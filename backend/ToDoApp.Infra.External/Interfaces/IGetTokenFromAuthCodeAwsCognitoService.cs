using ToDoApp.Core.Interfaces;
using ToDoApp.Infra.External.DTOs;

namespace ToDoApp.Infra.External.Interfaces;
public interface IGetTokenFromAuthCodeAwsCognitoService : IDefaultService
{
    public Task<AwsAuthenticationResultDTO?> Get(string authorizationCode);
}
