using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniRent.Application.Common.Interfaces;

namespace MiniRent.Application.Users.Queries.GetUserDetails;

public class GetUserDetailsQuery : IRequest<UserDetailsDto>
{
}


public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsDto>
{
    private readonly IMiniRentDbContext _miniRentDbContext;
    private readonly ICurrentUserService _currentUserService;

    public GetUserDetailsQueryHandler(IMiniRentDbContext miniRentDbContext, ICurrentUserService currentUserService)
    {
        _miniRentDbContext = miniRentDbContext;
        _currentUserService = currentUserService;
    }

    public Task<UserDetailsDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var login = _currentUserService.Login;

        var user = _miniRentDbContext.Renters
            .Include(r => r.Login)
            .Where(r => login != null && r.Login.Email.ToUpper() == login.ToUpper())
            .First();

        var userDto = new UserDetailsDto()
        {
            Id = user.Id,
            Address = user.Address,
            DateOfBirth = user.DateOfBirth,
            EmailAddress = user.EmailAddress,
            LicenceObtainmentYear = user.LicenceObtainmentYear,
            Name = user.Name,
            Surname = user.Surname,
            Login = user.Login.Email
        };
        return Task.FromResult(userDto);
    }
}