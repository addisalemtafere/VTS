using Application.Contracts;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class VehicleTrackingSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public VehicleTrackingSystemDbContext(DbContextOptions<VehicleTrackingSystemDbContext> dbContextOptions,
            ILoggedInUserService loggedInUserService) : base(
            dbContextOptions)
        {
            _loggedInUserService = loggedInUserService;
        }

        public DbSet<TrackingDevice> TrackingDevices { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancelationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _loggedInUserService.UserName;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.Now;
                        entry.Entity.ModifiedBy = _loggedInUserService.UserName;

                        break;
                }

            return base.SaveChangesAsync(cancelationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vehicle>(b => { b.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId); });

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}