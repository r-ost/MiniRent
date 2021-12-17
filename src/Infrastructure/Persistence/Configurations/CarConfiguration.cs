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
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(p => p.HorsePower).IsRequired();
            builder.Property(p => p.YearOfProduction).IsRequired();
            builder.Property(p => p.CompanyId).IsRequired();

            builder.Property(p => p.Description).HasMaxLength(10000);

            builder.OwnsOne(o => o.CarModel, c =>
            {
                c.WithOwner();
                c.Property(p => p.Brand).IsRequired();
                c.Property(p => p.Model).IsRequired();
            });
            //builder.Navigation(n => n.CarModel).IsRequired();
        }
    }
}