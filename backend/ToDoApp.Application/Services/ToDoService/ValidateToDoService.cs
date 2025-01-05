using ToDoApp.Application.DTOs.ToDoDTO;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Interfaces;
using ToDoApp.Core.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services.ToDoService;

internal class ValidateToDoService : DefaultService
{
    private readonly IToDoRepository _repository;
    private readonly string _requestUserId;

    public ValidateToDoService(
        IToDoRepository repository, 
        IGetRequestTokenDataService getRequestTokenDataService)
    {
        _repository = repository;
        _requestUserId = getRequestTokenDataService.GetUserId()!;
    }
    
    private async Task ValidateIfTaskIsFromCreationUser(Ulid id)
    {
        var entity = await _repository.GetById(id, _requestUserId);
        var creationUserId = entity?.UserId;
        var requestUserId = _requestUserId;

        Messages.AddErrorIf(
            creationUserId != requestUserId,
            "Você não pode editar uma tarefa criada por outra pessoa.");
    }

    private async Task ValidatePendingTasks()
    {
        var pendingTasks = await _repository.CountPending(_requestUserId);

        Messages.AddErrorIf(
            pendingTasks > 50,
            "Você tem muitas pendências. Conclua algumas antes de adicionar novas tarefas.");
    }

    private bool ValidateDTO(object? dto)
    {
        return !Messages.AddErrorIf(dto == null, "Dados devem ser informados.");
    }

    private void ValidateDescription(string? description)
    {
        var isDescriptionEmpty = Messages.AddErrorIf(
            string.IsNullOrWhiteSpace(description),
            "Descrição deve ser preenchida.");

        if (!isDescriptionEmpty)
        {
            var length = description!.Length;

            Messages.AddErrorIf(
                length < 3, 
                "Descrição deve conter ao menos três caractéres.");

            Messages.AddErrorIf(
                length > 100,
                "Descrição deve conter até cem caractéres.");
        }
    }

    public async Task<bool> ValidateExistence(string? id)
    {
        var exists = await _repository.Exists(Ulid.Parse(id), _requestUserId);

        Messages.AddErrorIf(
            !exists, 
            "Não existe registro de tarefa para o Id informado.");

        return exists;
    }

    public async Task<bool> Validate(CreateToDoDTO? dto)
    {
        if (!ValidateDTO(dto))
        {
            return false;
        }

        await ValidatePendingTasks();
        ValidateDescription(dto!.Description);

        return Messages.Count == 0;
    }

    public async Task<bool> Validate(UpdateToDoDTO? dto)
    {
        
        if (!ValidateDTO(dto))
        {
            return false;
        }

        if (ValidateId(dto!.Id))
        {
            await ValidateExistence(dto!.Id);
            await ValidateIfTaskIsFromCreationUser(Ulid.Parse(dto!.Id));
        }

        ValidateDescription(dto.Description);

        return Messages.Count == 0;
    }
}
