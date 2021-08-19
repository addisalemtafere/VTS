using Domain.Common;
using Domain.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class VehicleTrackingSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public VehicleTrackingSystemDbContext(DbContextOptions<VehicleTrackingSystemDbContext> dbContextOptions) : base(
            dbContextOptions)
        {
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
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                }

            return base.SaveChangesAsync(cancelationToken);
        }
    }
}