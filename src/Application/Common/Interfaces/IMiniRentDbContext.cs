using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniRent.Domain.Entities;
using MiniRent.Domain.ValueObjects;

namespace MiniRent.Application.Common.Interfaces
{
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
}