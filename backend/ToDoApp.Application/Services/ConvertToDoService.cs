using ToDoApp.Application.DTOs;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Services;
internal class ConvertToDoService
{
    public ToDo ConvertToEntity(CreateToDoDTO dto)
    {
        var entity = new ToDo();

        entity.Description = dto.Description!;
        entity.CreationDate = DateTime.Now;

        return entity;
    }

    public ReadToDoDTO ConverToDTO(ToDo entity)
    {
        var dto = new ReadToDoDTO();

        dto.Id = entity.Id.ToString();
        dto.Description = entity.Description;
        dto.CreationDate = entity.CreationDate;
        dto.CompletionDate = entity.CompletionDate;

        return dto;
    }

    public ToDo UpdateEntity(ToDo entity, UpdateToDoDTO dto)
    {
        entity.Id = Ulid.Parse(dto.Id);
        entity.Description = dto.Description!;
        entity.CompletionDate = DateTime.Now;

        return entity;
    }
}
