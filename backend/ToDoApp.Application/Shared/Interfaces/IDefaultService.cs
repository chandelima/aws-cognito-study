using ToDoApp.Application.Shared.Entities;

namespace ToDoApp.Application.Interfaces;

public interface IDefaultService
{
    public List<ResponseMessage> Messages { get; set; }
}
