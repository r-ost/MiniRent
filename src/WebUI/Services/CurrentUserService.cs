using System.Security.Claims;
using MiniRent.Application.Common.Interfaces;

namespace MiniRent.WebUI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Login
    {
        get 
        {
            return _httpContextAccessor.HttpContext?.User?.Claims.First(x => x.Type == "preferred_username").Value;
        }
    }

}
