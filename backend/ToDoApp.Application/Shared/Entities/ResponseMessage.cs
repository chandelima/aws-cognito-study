using ToDoApp.Application.Enumerators;

namespace ToDoApp.Application.Shared.Entities;
public class ResponseMessage
{
    public EnumMessageType Type { get; set; }
    public string Message { get; set; } = null!;
}

public static class ResponseMessageExtensions
{
    public static void AddError(this List<ResponseMessage> list, string message)
    {
        var entity = new ResponseMessage();

        entity.Type = EnumMessageType.Error;
        entity.Message = message;

        list.Add(entity);
    }
}