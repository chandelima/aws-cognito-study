using ToDoApp.Application.DTOs.ToDoDTO;
using ToDoApp.Core.Interfaces;

namespace ToDoApp.Application.Interfaces.ToDoService;

public interface ICreateToDoService : IDefaultService
{
    public Task Create(CreateToDoDTO? dto);
}
