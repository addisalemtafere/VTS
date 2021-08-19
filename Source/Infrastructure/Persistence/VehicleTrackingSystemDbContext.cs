using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VTS.Domain.Common;

namespace VTS.Persistence
{
    public class VehicleTrackingSystemDbContext : DbContext
    {
        public VehicleTrackingSystemDbContext(DbContextOptions<VehicleTrackingSystemDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<TrackingDevice> Devices { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancelationToken = new CancellationToken())
        {

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancelationToken);
        }

    }
}
