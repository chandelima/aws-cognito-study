using System.Text.Json.Serialization;

namespace ToDoApp.Core.Enumerators;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EnumMessageType
{
    Error,
    Information,
    Success
}
