using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;

public class Renter
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int LicenceObtainmentYear { get; set; }

    public int Age { get; set; }

    public int LoginId { get; set; }

    public Login Login { get; set; } = null!;

    public Address Address { get; set; } = Address.Empty;

    public List<Rent> Rents { get; private set; } = new List<Rent>();
}
