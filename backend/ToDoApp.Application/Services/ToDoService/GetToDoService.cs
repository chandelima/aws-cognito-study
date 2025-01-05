using ToDoApp.Application.DTOs.ToDoDTO;
using ToDoApp.Application.Interfaces.ToDoService;
using ToDoApp.Core.Interfaces;
using ToDoApp.Core.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services.ToDoService;

internal class GetToDoService : DefaultService, IGetToDoService
{
    private readonly IToDoRepository _repository;
    private readonly ConvertToDoService _convertService;
    private readonly string _requestUserId;

    public GetToDoService(
        IToDoRepository repository,
        ConvertToDoService convertService,
        IGetRequestTokenDataService getRequestTokenDataService)
    {
        _repository = repository;
        _convertService = convertService;
        _requestUserId = getRequestTokenDataService.GetUserId()!;
    }

    public async Task<List<ReadToDoDTO>?> GetAll()
    {
        var entities = await _repository.GetAllByUserId(_requestUserId);

        return entities
            .Select(x => _convertService.ConvertToDTO(x))
            .OrderBy(x => x.CompletionDate.HasValue)
            .ThenByDescending(x => x.CompletionDate)
            .ToList();
    }

    public async Task<ReadToDoDTO?> GetById(string id)
    {
        if (!ValidateId(id))
        {
            return null;
        }

        var entity = await _repository.GetById(Ulid.Parse(id), _requestUserId);
        if (entity == null)
        {
            return null;
        }

        var dto = _convertService.ConvertToDTO(entity!);

        return dto;
    }
}
