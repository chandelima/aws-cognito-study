using ToDoApp.Application.DTOs.ToDoDTO;
using ToDoApp.Core.Interfaces;

namespace ToDoApp.Application.Interfaces.ToDoService;

public interface IGetToDoService : IDefaultService
{
    public Task<List<ReadToDoDTO>?> GetAll();
    public Task<ReadToDoDTO?> GetById(string id);
}
