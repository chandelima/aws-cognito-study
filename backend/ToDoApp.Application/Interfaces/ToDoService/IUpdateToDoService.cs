using ToDoApp.Application.DTOs.ToDoDTO;
using ToDoApp.Core.Interfaces;

namespace ToDoApp.Application.Interfaces.ToDoService;

public interface IUpdateToDoService : IDefaultService
{
    public Task Update(UpdateToDoDTO? dto);
}
