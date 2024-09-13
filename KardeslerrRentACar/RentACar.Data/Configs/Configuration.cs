using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Core.Interfaces;
using RentACar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Configs
{
    public class RenterConfiguration : IEntityTypeConfiguration<Renter>
    {
        public void Configure(EntityTypeBuilder<Renter> builder)
        {
            builder.HasOne(r => r.User)   
               .WithOne()                    
               .HasForeignKey<Renter>(r => r.UserId) 
               .OnDelete(DeleteBehavior.Cascade);
        }
      
    }
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservations>
    {
        public void Configure(EntityTypeBuilder<Reservations> builder)
        {
           
            builder.HasOne(res => res.Renter)  
                   .WithMany(r => r.Reservations) 
                   .HasForeignKey(res => res.RenterId)
                   .OnDelete(DeleteBehavior.Cascade); 
            builder.HasOne(res => res.Vehicle)
                .WithMany(v => v.Reservations)
                .HasForeignKey(res => res.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class GarageConfiguration : IEntityTypeConfiguration<Garage>
    {
       public void Configure(EntityTypeBuilder<Garage> builder)
        {
            builder.HasMany(g => g.Employers)
                .WithOne(e => e.Garage)
                .HasForeignKey(e => e.GarageId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(g => g.Vehicles)
                .WithOne(v => v.Garage)
                .HasForeignKey(v => v.GarageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(g => g.BalanceSheet)
            .HasColumnType("decimal(18,4)");
        }
    }
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(p => p.User)
                .WithMany(u => u.Payment)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
