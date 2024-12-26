using ToDoApp.Application.Shared.Entities;

namespace ToDoApp.API.Entities;

public class DafaultResponse
{
    public DafaultResponse(List<ResponseMessage>? messages = null)
    {
        if (messages != null)
        {
            Messages = messages;
        }
    }

    public DafaultResponse(dynamic? data, List<ResponseMessage>? messages = null)
    {
        Data = data;

        if (messages != null)
        {
            Messages = messages;
        }
    }

    public dynamic? Data { get; set; }
    public List<ResponseMessage>? Messages { get; set; }
}
