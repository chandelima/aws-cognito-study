using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ToDoApp.Core.Interfaces;

namespace ToDoApp.Core.Services;
internal class GetRequestTokenDataService : IGetRequestTokenDataService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetRequestTokenDataService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetToken()
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        return token;
    }

    public string? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId;
    }
}
