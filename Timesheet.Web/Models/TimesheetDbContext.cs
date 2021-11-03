using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Web.Models
{
    public class TimesheetDbContext : DbContext
    {
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }

        public TimesheetDbContext(DbContextOptions<TimesheetDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Timesheet>()
                .HasOne(a => a.Location)
                .WithMany()
                .HasForeignKey(b => b.LocationId);
            builder.Entity<Timesheet>()
    .HasOne(a => a.Staff)
    .WithMany()
    .HasForeignKey(b => b.StaffId);
            builder.Entity<Timesheet>()
    .HasOne(a => a.Client)
    .WithMany()
    .HasForeignKey(b => b.ClientId);


            builder.Entity<Staff>().HasData(
                new Staff { StaffId = 1, Surname = "Davison", Forename = "Tyrone", Email = "tyrone.davison@example.com"},
                new Staff { StaffId = 2, Surname = "Khan", Forename = "Zafar", Email = "zafar.khan@example.com" },
                new Staff { StaffId = 3, Surname = "Fairbairn", Forename = "James", Email = "james.fairbairn@example.com" }
            );

            builder.Entity<Client>().HasData(
                new Client { ClientId = 1, BillingAddress = "3 Hopeless Lane, Fiddler District, Middlesbrough", CompanyName = "Fiddler Fingers", ContactName = "Free Willy", ContactTelephone = "01234 121212", ContactEmail = "fw@example.com" },
                new Client { ClientId = 2, BillingAddress = "42 Cloud Lane, Cloud District, Middlesbrough", CompanyName = "Pie in the Sky", ContactName = "Sally Pie", ContactTelephone = "01234 341245", ContactEmail = "sally@example.com" }
            );

            builder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Willy's Hovel", Address = "4 Wonky Way, Middlesbrough" },
                new Location { LocationId = 2, Name = "Fiddler Fingers Head Office", Address = "3 Hopeless Lane, Fiddler District, Middlesbrough" },
                new Location { LocationId = 3, Name = "Fiddler Fingers Warehouse", Address = "13 Stack It Road, Storage District, Middlesbrough" },
                new Location { LocationId = 4, Name = "Sally's Place", Address = "1 Crust Avenue, Busy District, Middlesbrough" },
                new Location { LocationId = 5, Name = "Sally's Takeout", Address = "3 Crust Avenue, Busy District, Middlesbrough" }
            );

            builder.Entity<Timesheet>().HasData(
                new Timesheet { TimesheetId = 1, MinutesWorked = 80, StaffId = 1, ClientId = 2, LocationId = 4 }
            );
        }
    }
}
