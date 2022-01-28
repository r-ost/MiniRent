using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRent.Domain.Entities;

namespace MiniRent.Infrastructure.Persistence.Configurations;

public class FinalizedRentDetailsConfiguration : IEntityTypeConfiguration<FinalizedRentDetails>
{

    public void Configure(EntityTypeBuilder<FinalizedRentDetails> builder)
    {
        builder.Property(o => o.RentId).IsRequired();
    }
}
