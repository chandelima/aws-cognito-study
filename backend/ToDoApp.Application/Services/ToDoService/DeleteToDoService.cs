using ToDoApp.Application.Interfaces.ToDoService;
using ToDoApp.Core.Interfaces;
using ToDoApp.Core.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services.ToDoService;

internal class DeleteToDoService : DefaultService, IDeleteToDoService
{
    private readonly IToDoRepository _repository;
    private readonly ValidateToDoService _validateToDoService;
    private readonly string _requestUserId;

    public DeleteToDoService(
        IToDoRepository repository,
        ValidateToDoService validateToDoService,
        IGetRequestTokenDataService getRequestTokenDataService)
    {
        _repository = repository;
        _validateToDoService = validateToDoService;
        _requestUserId = getRequestTokenDataService.GetUserId()!;
    }

    public async Task<bool> Delete(string id)
    {
        if (!ValidateId(id))
        {
            return false;
        }

        if (!await _validateToDoService.ValidateExistence(id))
        {
            Messages = _validateToDoService.Messages;
            return false;
        }

        return await _repository.Delete(Ulid.Parse(id), _requestUserId);
    }
}
