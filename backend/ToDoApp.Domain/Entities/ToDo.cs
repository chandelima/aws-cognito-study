namespace ToDoApp.Domain.Entities;

public class ToDo : Base
{
    public string Description { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime? CompletionDate { get; set; }
}
