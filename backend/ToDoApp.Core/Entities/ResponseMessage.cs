using ToDoApp.Core.Enumerators;

namespace ToDoApp.Core.Entities;
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
    public static bool AddErrorIf(this List<ResponseMessage> list, bool condition, string message)
    {
        if (condition)
        {
            AddError(list, message);
        }

        return condition;
    }
}