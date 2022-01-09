using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MiniRent.Application.Common.Interfaces;

namespace MiniRent.Application.Users.Queries.GetUserDetails;

public class GetUserDetailsQueryValidator: AbstractValidator<GetUserDetailsQuery>
{
    private readonly IMiniRentDbContext _miniRentDbContext;
    private readonly ICurrentUserService _currentUserService;

    public GetUserDetailsQueryValidator(IMiniRentDbContext miniRentDbContext,
        ICurrentUserService currentUserService)
    {
        _miniRentDbContext = miniRentDbContext;
        _currentUserService = currentUserService;

        var login = _currentUserService.Login;
        RuleFor(x => login)
            .MustAsync(LoginExists).WithMessage("User with provided login doesn't exist.");
    }


    public Task<bool> LoginExists(string? login, CancellationToken cancellationToken)
    {
        var temp = _miniRentDbContext.Logins
            .FirstOrDefault(x => x.Email == login);

        return Task.FromResult(temp is not null);
    }
}
