using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRent.Domain.Entities;

namespace MiniRent.Infrastructure.Persistence.Configurations
{
    public class RenterConfiguration : IEntityTypeConfiguration<Renter>
    {
        public void Configure(EntityTypeBuilder<Renter> builder)
        {
            builder.OwnsOne(x => x.Address);
        }
    }
}