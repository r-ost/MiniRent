using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Domain.ValueObjects;

namespace MiniRent.Domain.Entities
{
    public class Worker
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = String.Empty;

        public int LoginId { get; set; }

        public int CompanyId { get; set; }

        public Login Login { get; set; } = null!;

        public Company Company { get; set; } = null!;

        public Address Address { get; set; } = Address.Empty;
    }
}