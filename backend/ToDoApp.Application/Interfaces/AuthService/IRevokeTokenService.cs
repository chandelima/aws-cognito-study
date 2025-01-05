using ToDoApp.Core.DTOs;
using ToDoApp.Core.Interfaces;

namespace ToDoApp.Application.Interfaces.AuthService;
public interface IRevokeTokenService : IDefaultService
{
    public Task Revoke(ValueDTO<string> dto);
}
