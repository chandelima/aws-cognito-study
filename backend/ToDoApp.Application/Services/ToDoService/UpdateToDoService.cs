using ToDoApp.Application.DTOs.ToDoDTO;
using ToDoApp.Application.Interfaces.ToDoService;
using ToDoApp.Core.Interfaces;
using ToDoApp.Core.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services.ToDoService;

internal class UpdateToDoService : DefaultService, IUpdateToDoService
{
    private readonly IToDoRepository _repository;
    private readonly ValidateToDoService _validateService;
    private readonly ConvertToDoService _convertToDoService;
    private readonly string _requestUserId;

    public UpdateToDoService(
        IToDoRepository repository,
        ValidateToDoService validateService,
        ConvertToDoService convertToDoService,
        IGetRequestTokenDataService getRequestTokenDataService)
    {
        _repository = repository;
        _validateService = validateService;
        _convertToDoService = convertToDoService;
        _requestUserId = getRequestTokenDataService.GetUserId()!;
    }

    public async Task Update(UpdateToDoDTO? dto)
    {
        if (!await _validateService.Validate(dto))
        {
            Messages = _validateService.Messages;
            return;
        }

        var entity = await _repository.GetById(Ulid.Parse(dto!.Id), _requestUserId);
        var updatedEntity = _convertToDoService.UpdateEntity(entity!, dto);

        await _repository.Update(updatedEntity);
    }
}
