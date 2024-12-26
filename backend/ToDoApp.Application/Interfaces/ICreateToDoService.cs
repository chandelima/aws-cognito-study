using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Interfaces;

public interface ICreateToDoService : IDefaultService
{
    public Task Create(CreateToDoDTO? dto);
}
