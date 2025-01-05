using ToDoApp.Core.Interfaces;

namespace ToDoApp.Application.Interfaces.ToDoService;

public interface IDeleteToDoService : IDefaultService
{
    public Task<bool> Delete(string id);
}
