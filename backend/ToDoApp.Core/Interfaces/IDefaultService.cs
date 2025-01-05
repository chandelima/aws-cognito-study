using ToDoApp.Core.Entities;

namespace ToDoApp.Core.Interfaces;

public interface IDefaultService
{
    public List<ResponseMessage> Messages { get; set; }
}
