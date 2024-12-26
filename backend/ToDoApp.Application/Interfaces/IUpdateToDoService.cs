using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Interfaces;

public interface IUpdateToDoService : IDefaultService
{
    public Task Update(UpdateToDoDTO? dto);
}
