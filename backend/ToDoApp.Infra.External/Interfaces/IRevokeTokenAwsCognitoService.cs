using ToDoApp.Core.Interfaces;

namespace ToDoApp.Infra.External.Interfaces;

public interface IRevokeTokenAwsCognitoService : IDefaultService
{
    public Task Revoke(string refreshToken);
}
