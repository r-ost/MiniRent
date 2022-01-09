using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniRent.Application.Users.Commands.CreateUser;
using MiniRent.Application.Users.Queries.GetUserDetails;

namespace MiniRent.WebUI.Controllers;



[Authorize]
public class UserController : ApiControllerBase
{
    [HttpGet]
    public async Task<UserDetailsDto> GetUserDetails()
    {
        return await Mediator.Send(new GetUserDetailsQuery());
    }


    [HttpPost]
    public async Task<int> Create([FromBody]CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
