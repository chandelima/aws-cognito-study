using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Shared.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services;

internal class GetToDoService : DefaultService, IGetToDoService
{
    private readonly IToDoRepository _repository;
    private readonly ConvertToDoService _convertService;

    public GetToDoService(IToDoRepository repository)
    {
        _repository = repository;
        _convertService = new();
    }

    public async Task<List<ReadToDoDTO>?> GetAll()
    {
        var entities = await _repository.GetAll();

        if (entities.Count == 0)
        {
            return null;
        }

        return entities
            .Select(x => _convertService.ConverToDTO(x))
            .ToList();
    }

    public async Task<ReadToDoDTO?> GetById(string id)
    {
        if (!ValidateId(id))
        {
            return null;
        }

        var entity = await _repository.GetById(Ulid.Parse(id));
        if (entity == null)
        {
            return null;
        }

        var dto = _convertService.ConverToDTO(entity!);

        return dto;
    }
}
