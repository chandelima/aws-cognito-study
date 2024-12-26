using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Shared.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services;

internal class UpdateToDoService : DefaultService, IUpdateToDoService
{
    private readonly IToDoRepository _repository;
    private readonly ValidateToDoService _validateService;

    public UpdateToDoService(
        IToDoRepository repository, 
        ValidateToDoService validateService)
    {
        _repository = repository;
        _validateService = validateService;
    }

    public async Task Update(UpdateToDoDTO? dto)
    {
        if (!await _validateService.Validate(dto))
        {
            this.Messages = _validateService.Messages;
            return;
        }

        var entity = await _repository.GetById(Ulid.Parse(dto.Id));

        var convert = new ConvertToDoService();
        var updatedEntity = convert.UpdateEntity(entity!, dto);

        await _repository.Update(updatedEntity);
    }
}
