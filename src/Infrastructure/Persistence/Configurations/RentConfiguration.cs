using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRent.Domain.Entities;

namespace MiniRent.Infrastructure.Persistence.Configurations;

public class RentConfiguration : IEntityTypeConfiguration<Rent>
{

    public void Configure(EntityTypeBuilder<Rent> builder)
    {
        builder.Property(p => p.StartDate).IsRequired();
        builder.Property(p => p.EndDate);
        builder.Property(p => p.RentStatus).IsRequired();
        builder.Property(p => p.RenterId).IsRequired();
        builder.Property(p => p.CarId).IsRequired().HasMaxLength(128);
    }
}
