namespace ToDoApp.Application.Interfaces;

public interface IDeleteToDoService : IDefaultService
{
    public Task<bool> Delete(string id);
}
