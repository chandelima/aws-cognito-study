using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Interfaces;

public interface IGetToDoService : IDefaultService
{
    public Task<List<ReadToDoDTO>?> GetAll();
    public Task<ReadToDoDTO?> GetById(string id);
}
