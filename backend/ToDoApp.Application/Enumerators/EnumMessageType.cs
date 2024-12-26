using System.Text.Json.Serialization;

namespace ToDoApp.Application.Enumerators;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EnumMessageType
{
    Error,
    Information,
    Success
}
