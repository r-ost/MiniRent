using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiniRent.Application.Common.Interfaces;
using MiniRent.Domain.Entities;
using MiniRent.Domain.ValueObjects;

namespace MiniRent.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address Address { get; set; } = Address.Empty;
    public DateTime DateOfBirth { get; set; }
    public int DriverLicenseObtainmentYear { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IMiniRentDbContext _miniRentDbContext;
    private readonly ICurrentUserService _currentUserService;

    public CreateUserCommandHandler(IMiniRentDbContext miniRentDbContext,
        ICurrentUserService currentUserService)
    {
        _miniRentDbContext = miniRentDbContext;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userLogin = _currentUserService.Login;

        var login = new Login() { Email = userLogin ?? "" };


        _miniRentDbContext.Logins
            .Add(login);

        await _miniRentDbContext.SaveChangesAsync(cancellationToken);

        var user = new Renter()
        {
            LoginId = login.Id,
            Address = request.Address,
            EmailAddress = request.Email,
            DateOfBirth = request.DateOfBirth,
            LicenceObtainmentYear = request.DriverLicenseObtainmentYear,
            Name = request.FirstName,
            Surname = request.Surname
        };


        _miniRentDbContext.Renters
            .Add(user);

        await _miniRentDbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
