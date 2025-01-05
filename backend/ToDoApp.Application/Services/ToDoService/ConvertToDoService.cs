using ToDoApp.Application.DTOs.ToDoDTO;
using ToDoApp.Core.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Services.ToDoService;
internal class ConvertToDoService
{
    private readonly IGetRequestTokenDataService _requestTokenDataService;

    public ConvertToDoService(IGetRequestTokenDataService requestTokenDataService)
    {
        _requestTokenDataService = requestTokenDataService;
    }

    public ToDo ConvertToEntity(CreateToDoDTO dto)
    {
        var entity = new ToDo();

        entity.Description = dto.Description!;
        entity.CreationDate = DateTime.Now;
        entity.UserId = _requestTokenDataService.GetUserId()!;

        return entity;
    }

    public ReadToDoDTO ConvertToDTO(ToDo entity)
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
        entity.CompletionDate = dto.CompletionDate;

        return entity;
    }
}
