using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces;
public interface IToDoRepository
{
    public Task Create(ToDo entity);
    public Task<List<ToDo>> GetAllByUserId(string userId);
    public Task<ToDo?> GetById(Ulid id, string userId);
    public Task Update(ToDo entity);
    public Task<bool> Delete(Ulid id, string userId);
    public Task<bool> Exists(Ulid id, string userId);
    public Task<int> CountPending(string userId);
}
