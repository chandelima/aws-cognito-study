﻿namespace ToDoApp.Application.DTOs;
public class ReadToDoDTO
{
    public string Id { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime? CompletionDate { get; set; }
}