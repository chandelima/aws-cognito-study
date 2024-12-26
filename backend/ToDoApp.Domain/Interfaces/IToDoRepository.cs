using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces;
public interface IToDoRepository
{
    public Task Create(ToDo entity);
    public Task<List<ToDo>> GetAll();
    public Task<ToDo?> GetById(Ulid id);
    public Task Update(ToDo entity);
    public Task<bool> Delete(Ulid id);
    public Task<bool> Exists(Ulid id);
    public Task<int> CountPending();
}
