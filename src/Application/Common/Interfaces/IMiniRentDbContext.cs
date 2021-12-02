using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IMiniRentDbContext
{
    public DbSet<Login> Logins { get; }
    public DbSet<Address> Addresss { get; }
    public DbSet<CarModel> CarModels { get; }
    public DbSet<Car> Cars { get; }
    public DbSet<Company> Companys { get; }
    public DbSet<Rent> Rents { get; }
    public DbSet<Renter> Renters { get; }
    public DbSet<Worker> Workers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
