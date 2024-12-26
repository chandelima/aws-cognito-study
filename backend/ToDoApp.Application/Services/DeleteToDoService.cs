using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Shared.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services;

internal class DeleteToDoService : DefaultService, IDeleteToDoService
{
    private readonly IToDoRepository _repository;
    private readonly ValidateToDoService _validateToDoService;

    public DeleteToDoService(
        IToDoRepository repository, 
        ValidateToDoService validateToDoService)
    {
        _repository = repository;
        _validateToDoService = validateToDoService;
    }

    public async Task<bool> Delete(string id)
    {
        if (!ValidateId(id))
        {
            return false;
        }

        if (!await _validateToDoService.ValidateExistence(id))
        {
            this.Messages = _validateToDoService.Messages;
            return false;
        }

        return await _repository.Delete(Ulid.Parse(id));
    }
}
