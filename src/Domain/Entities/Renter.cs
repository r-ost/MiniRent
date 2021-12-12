using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Domain.ValueObjects;

namespace MiniRent.Domain.Entities
{
    public class Renter
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public int LicenceObtainmentYear { get; set; }

        public int Age { get; set; }

        public string EmailAddress { get; set; } = string.Empty;

        public int LoginId { get; set; }

        public Login Login { get; set; } = null!;

        public Address Address { get; set; } = Address.Empty;

        public List<Rent> Rents { get; private set; } = new List<Rent>();
    }
}