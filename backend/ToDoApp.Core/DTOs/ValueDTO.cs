namespace ToDoApp.Core.DTOs;
public class ValueDTO<T>
{
    public ValueDTO(T value)
    {
        Value = value;
    }

    public T? Value { get; set; }
}
