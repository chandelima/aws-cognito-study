using ToDoApp.Application.Shared.Entities;

namespace ToDoApp.Application.Shared.Services;
public class DefaultService
{
    public List<ResponseMessage> Messages { get; set; } = [];

    public bool ValidateId(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            Messages.AddError("Id deve ser informado.");
            return false;
        }

        if (!Ulid.TryParse(id, out var _))
        {
            Messages.AddError("Id informado inválido.");
            return false;
        }

        return true;
    }
}
