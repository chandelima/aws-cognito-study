namespace ToDoApp.Core.Interfaces;
public interface IGetRequestTokenDataService
{
    public string? GetToken();
    public string? GetUserId();
}
