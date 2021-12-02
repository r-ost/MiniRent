using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Infrastructure.Identity;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Entities;

public class MiniRentDbContext : ApiAuthorizationDbContext<ApplicationUser>, IMiniRentDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;
    private readonly IDomainEventService _domainEventService;

    private string _connectionString =
        $"Data Source=dev-mini-rent-sqlserver.database.windows.net;Initial Catalog=RentalDB;Persist Security Info=True;" +
        $"User ID=ef_migration;Password=Entity123";

    public DbSet<Login> Logins => Set<Login>();
    public DbSet<Address> Addresss => Set<Address>();
    public DbSet<CarModel> CarModels => Set<CarModel>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Company> Companys => Set<Company>();
    public DbSet<Rent> Rents => Set<Rent>();
    public DbSet<Renter> Renters => Set<Renter>();
    public DbSet<Worker> Workers => Set<Worker>();

    public MiniRentDbContext(
     DbContextOptions<MiniRentDbContext> options,
     IOptions<OperationalStoreOptions> operationalStoreOptions,
     ICurrentUserService currentUserService,
     IDomainEventService domainEventService,
     IDateTime dateTime) : base(options, operationalStoreOptions)
    {
        _currentUserService = currentUserService;
        _domainEventService = domainEventService;
        _dateTime = dateTime;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}
