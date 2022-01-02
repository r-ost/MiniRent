using Microsoft.AspNetCore.Identity;
using MiniRent.Domain.Entities;
using MiniRent.Domain.ValueObjects;


namespace MiniRent.Infrastructure.Persistence;

public static class MiniDbContextSeed
{
    public static async Task SeedDefaultUserAsync()
    {
        //var administratorRole = new IdentityRole("Administrator");

        //if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        //{
        //    await roleManager.CreateAsync(administratorRole);
        //}

        //var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        //if (userManager.Users.All(u => u.UserName != administrator.UserName))
        //{
        //    await userManager.CreateAsync(administrator, "Administrator1!");
        //    await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        //}
    }

    public static async Task SeedSampleDataAsync(MiniRentDbContext context)
    {
        //Seed, if necessary
        if (!context.Cars.Any())
        {
            context.Cars.Add(new Car
            {
                HorsePower = 100,
                YearOfProduction = 2006,
                Description = "najtanszy ale i niezawodny",
                CompanyId = 1,
                CarModel = new CarModel("toyota", "corolla"),
                Company = null!
            });

            context.Cars.Add(new Car
            {
                HorsePower = 110,
                YearOfProduction = 2008,
                Description = "taki no, sredni",
                CompanyId = 1,
                CarModel = new CarModel("ford", "focus"),
                Company = null!
            });

            context.Cars.Add(new Car
            {
                HorsePower = 220,
                YearOfProduction = 2018,
                Description = "nowy piekny szybki samochod",
                CompanyId = 1,
                CarModel = new CarModel("vovlo", "xc60"),
                Company = null!
            });

            context.Cars.Add(new Car
            {
                HorsePower = 130,
                YearOfProduction = 2015,
                Description = "duzy samochod, dobry dla rodziny lub budki z wata cukrowa",
                CompanyId = 1,
                CarModel = new CarModel("scoda", "octavia"),
                Company = null!
            });
            await context.SaveChangesAsync();
        }
        if (!context.Companys.Any())
        {
            context.Companys.Add(new Company
            {
                Name = "Samochodex",
            }
            );
            context.Companys.Add(new Company
            {
                Name = "Auto-Auto inc."
            }
            );
            context.Companys.Add(new Company
            {
                Name = "Wypożyczalnia u Stefana"
            }
            );
            await context.SaveChangesAsync();
        }
        if (!context.Logins.Any())
        {
            context.Logins.Add(new Login
            {
                Email = "jurek@tlen.pl"
            }
            );
            context.Logins.Add(new Login
            {
                Email = "franek@gmail.com"
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
                StartDate = new DateTime()
            }
            ); ;
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
