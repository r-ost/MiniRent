﻿using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Persistence;

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
        //if (!context.Cars.Any())
        //    {
        //        context.TodoLists.Add(new TodoList
        //        {
        //            Title = "Shopping",
        //            Colour = Colour.Blue,
        //            Items =
        //            {
        //                new TodoItem { Title = "Apples", Done = true },
        //                new TodoItem { Title = "Milk", Done = true },
        //                new TodoItem { Title = "Bread", Done = true },
        //                new TodoItem { Title = "Toilet paper" },
        //                new TodoItem { Title = "Pasta" },
        //                new TodoItem { Title = "Tissues" },
        //                new TodoItem { Title = "Tuna" },
        //                new TodoItem { Title = "Water" }
        //            }
        //        });

        //        await context.SaveChangesAsync();
        //    }
    }
}
