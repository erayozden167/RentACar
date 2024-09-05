using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RentACar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employers { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Renter> Renters { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
