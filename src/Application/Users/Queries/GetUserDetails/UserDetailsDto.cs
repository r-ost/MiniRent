using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Domain.ValueObjects;

namespace MiniRent.Application.Users.Queries.GetUserDetails;

public class UserDetailsDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;

    public int LicenceObtainmentYear { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string EmailAddress { get; set; } = string.Empty;
    public Address Address { get; set; } = Address.Empty;
}
