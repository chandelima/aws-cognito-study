using ToDoApp.Application.DTOs;
using ToDoApp.Application.Shared.Entities;
using ToDoApp.Application.Shared.Services;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services;

internal class ValidateToDoService : DefaultService
{
    private readonly IToDoRepository _repository;

    public ValidateToDoService(IToDoRepository repository)
    {
        _repository = repository;
    }

    private async Task ValidatePendingTasks()
    {
        var pendingTasks = await _repository.CountPending();

        if (pendingTasks > 50)
        {
            Messages.AddError("Você tem muitas pendências. Conclua algumas antes de adicionar novas tarefas.");
        }
    }

    private bool ValidateDTO(object? dto)
    {
        if (dto == null)
        {
            Messages.AddError("Não foi recebido nenhum objeto.");
            return false;
        }

        return true;
    }
    
    private void ValidateDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Messages.AddError("Descrição deve ser preenchida.");
        }
        else
        {
            var length = description.Length;

            if (length < 3)
            {
                Messages.AddError("Descrição deve conter ao menos três caractéres.");
            }

            if (length > 100)
            {
                Messages.AddError("Descrição deve conter até cem caractéres.");
            }
        }
    }

    public async Task<bool> ValidateExistence(string? id)
    {
        if (!await _repository.Exists(Ulid.Parse(id)))
        {
            Messages.AddError("Não existe registro de tarefa para o Id informado.");
            return false;
        }

        return true;
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
        }

        ValidateDescription(dto.Description);

        return Messages.Count == 0;
    }
}
