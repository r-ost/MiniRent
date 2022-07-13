using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRent.Domain.Entities;

namespace MiniRent.Infrastructure.Persistence.Configurations;

public class RenterConfiguration : IEntityTypeConfiguration<Renter>
{
    public void Configure(EntityTypeBuilder<Renter> builder)
    {
        builder.Property(c => c.Name).IsRequired().HasMaxLength(30);
        builder.Property(c => c.Surname).IsRequired().HasMaxLength(30);
        builder.Property(c => c.LicenceObtainmentYear).IsRequired();
        builder.Property(c => c.DateOfBirth).IsRequired();
        builder.Property(c => c.LoginId).IsRequired();

        builder.OwnsOne(x => x.Address, c =>
        {
            c.Property(p => p.Street).IsRequired().HasColumnName("Street");
            c.Property(p => p.City).IsRequired().HasColumnName("City");
            c.Property(p => p.Country).IsRequired().HasColumnName("Country");
            c.Property(p => p.ZipCode).IsRequired().HasColumnName("ZipCode");
        });
    }
}
