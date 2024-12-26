using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Shared.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services;
internal class CreateToDoService : DefaultService, ICreateToDoService
{
    private readonly IToDoRepository _repository;
    private readonly ValidateToDoService _validateService;

    public CreateToDoService(
        IToDoRepository repository, 
        ValidateToDoService validateService)
    {
        _repository = repository;
        _validateService = validateService;
    }

    public async Task Create(CreateToDoDTO? dto)
    {
        if (!await _validateService.Validate(dto))
        {
            this.Messages = _validateService.Messages;
            return;
        }

        var convert = new ConvertToDoService();
        var entity = convert.ConvertToEntity(dto);

        await _repository.Create(entity);
    }
}
