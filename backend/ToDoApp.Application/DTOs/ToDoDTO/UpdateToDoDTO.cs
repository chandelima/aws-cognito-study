namespace ToDoApp.Application.DTOs.ToDoDTO;
public class UpdateToDoDTO
{
    public string? Id { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public DateTime? CompletionDate { get; set; }
}
