using ToDoApp.Application.DTOs.ToDoDTO;
using ToDoApp.Application.Interfaces.ToDoService;
using ToDoApp.Core.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services.ToDoService;
internal class CreateToDoService : DefaultService, ICreateToDoService
{
    private readonly IToDoRepository _repository;
    private readonly ValidateToDoService _validateService;
    private readonly ConvertToDoService _convertToDoService;

    public CreateToDoService(
        IToDoRepository repository,
        ValidateToDoService validateService,
        ConvertToDoService convertToDoService)
    {
        _repository = repository;
        _validateService = validateService;
        _convertToDoService = convertToDoService;
    }

    public async Task Create(CreateToDoDTO? dto)
    {
        if (!await _validateService.Validate(dto))
        {
            Messages = _validateService.Messages;
            return;
        }

        var entity = _convertToDoService.ConvertToEntity(dto!);

        await _repository.Create(entity);
    }
}
