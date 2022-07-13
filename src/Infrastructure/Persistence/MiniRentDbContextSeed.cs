using Microsoft.AspNetCore.Identity;
using MiniRent.Domain.Entities;
using MiniRent.Domain.ValueObjects;


namespace MiniRent.Infrastructure.Persistence;

public static class MiniDbContextSeed
{
    public static async Task SeedSampleDataAsync(MiniRentDbContext context)
    {
        //Seed, if necessary
        if (!context.Companies.Any())
        {
            context.Companies.Add(new Company
            {
                Name = "Samochodex",
            }
            );
            context.Companies.Add(new Company
            {
                Name = "Auto-Auto inc."
            }
            );
            context.Companies.Add(new Company
            {
                Name = "Wypożyczalnia u Stefana"
            }
            );
            context.Companies.Add(new Company
            {
                Name = "lecturer-api"
            }
            );
            context.Companies.Add(new Company
            {
                Name = "Auto-Land-api"
            });
            await context.SaveChangesAsync();
        }
        if (!context.Logins.Any())
        {
            context.Logins.Add(new Login
            {
                Email = "jan.szablanowski@gmail.com"
            }
            );
            context.Logins.Add(new Login
            {
                Email = "01151574@pw.edu.pl"
            }
             );
            context.Logins.Add(new Login
            {
                Email = "agata@pw.edu.pl"
            }
             );
            context.Logins.Add(new Login
            {
                Email = "krzys@wp.pl"
            }
             );
            await context.SaveChangesAsync();
        }
        if (!context.Renters.Any())
        {

            context.Renters.Add(new Renter
            {
                Name = "Jerzy",
                Surname = "Kowalski",
                LicenceObtainmentYear = 1983,
                DateOfBirth = new DateTime(1962, 12, 12),
                LoginId = 1
            }
            );
            context.Renters.Add(new Renter
            {
                Name = "Franciszek",
                Surname = "Nowak",
                LicenceObtainmentYear = 2018,
                DateOfBirth = new DateTime(1969, 11, 1),
                LoginId = 2
            }
             );
            await context.SaveChangesAsync();
        }
        if (!context.Rents.Any())
        {
            context.Rents.Add(new Rent
            {
                StartDate = new DateTime(),
                RenterId = 1,
                CarId = Guid.NewGuid().ToString(),
                CompanyId = 1
            }
            );
            await context.SaveChangesAsync();
        }
        if (!context.Workers.Any())
        {
            context.Workers.Add(new Worker
            {
                Name = "Agata",
                Surname = "Wachowska",
                LoginId = 3,
                CompanyId = 1
            }
            );
            context.Workers.Add(new Worker
            {
                Name = "Krzysztof",
                Surname = "Danielak",
                LoginId = 3,
                CompanyId = 1
            }
            );


            await context.SaveChangesAsync();
        }
    }
}
