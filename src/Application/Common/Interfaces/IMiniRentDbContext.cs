using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniRent.Domain.Entities;
using MiniRent.Domain.ValueObjects;

namespace MiniRent.Application.Common.Interfaces;

public interface IMiniRentDbContext
{
    public DbSet<Login> Logins { get; }
    public DbSet<Company> Companys { get; }
    public DbSet<Rent> Rents { get; }
    public DbSet<Renter> Renters { get; }
    public DbSet<Worker> Workers { get; }   
    public DbSet<FinalizedRentDetails> FinalizedRentDetails { get; }   

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
