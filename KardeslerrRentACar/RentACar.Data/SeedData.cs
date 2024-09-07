using RentACar.Core.Interfaces;
using RentACar.Data;
using RentACar.Domain;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Name = "John Doe", Email = "john.doe@example.com", PasswordHashed = "hashedpassword1", Role = "Admin" },
                new User { Name = "Jane Smith", Email = "jane.smith@example.com", PasswordHashed = "hashedpassword2", Role = "User" }
            );
            context.SaveChanges();
        }

        var garage = context.Garages.FirstOrDefault();
        if (garage == null)
        {
            garage = new Garage
            {
                GarageName = "Main Garage",
                Address = "Downtown",
                EstablishDate = new DateTime(2000, 1, 1),
                BalanceSheet = 10000.00m
            };
            context.Garages.Add(garage);
            context.SaveChanges();
        }

        var user1 = context.Users.FirstOrDefault(u => u.Email == "john.doe@example.com");
        var user2 = context.Users.FirstOrDefault(u => u.Email == "jane.smith@example.com");

        if (!context.Employers.Any())
        {
            context.Employers.AddRange(
                new Employee { GovIdNumber = "123456789", LicenseType = "A", DateOfBirth = new DateTime(1985, 5, 15), PhoneNumber = "555-1234", Role = "Manager", Address = "123 Elm Street", UserId = user1.UserId, GarageId = garage.Id, Garage = garage },
                new Employee { GovIdNumber = "987654321", LicenseType = "B", DateOfBirth = new DateTime(1990, 7, 22), PhoneNumber = "555-5678", Role = "Driver", Address = "456 Oak Avenue", UserId = user2.UserId, GarageId = garage.Id, Garage = garage }
            );
            context.SaveChanges();
        }

        if (!context.Renters.Any())
        {
            context.Renters.AddRange(
                new Renter { GovIdNumber = "112233445", LicenseType = "C", DateOfBirth = new DateTime(1980, 2, 28), PhoneNumber = "555-6789", Address = "789 Pine Road", UserId = user1.UserId },
                new Renter { GovIdNumber = "556677889", LicenseType = "D", DateOfBirth = new DateTime(1995, 9, 10), PhoneNumber = "555-9876", Address = "101 Maple Drive", UserId = user2.UserId }
            );
            context.SaveChanges();
        }

        if (!context.Vehicles.Any())
        {
            context.Vehicles.AddRange(
                new Vehicle { Name = "Sedan", Status = "Available", LicensePlate = "ABC123", Brand = "Toyota", Color = "Red", FuelType = "Gasoline", Hp = 150, VehicleType = "Car", Year = 2020, Kms = 15000, RentalPrice = 75.00, DateForRenting = DateTime.Now, GarageId = garage.Id, Garage = garage },
                new Vehicle { Name = "SUV", Status = "Available", LicensePlate = "XYZ789", Brand = "Honda", Color = "Blue", FuelType = "Diesel", Hp = 200, VehicleType = "SUV", Year = 2021, Kms = 5000, RentalPrice = 100.00, DateForRenting = DateTime.Now, GarageId = garage.Id, Garage = garage }
            );
            context.SaveChanges();
        }

        if (!context.Reservations.Any())
        {
            var firstVehicle = context.Vehicles
                .OrderBy(v => v.Id)
                .FirstOrDefault();

            var lastVehicle = context.Vehicles
                .OrderBy(v => v.Id)
                .LastOrDefault();

            var firstRenter = context.Renters
                .OrderBy(r => r.Id)
                .FirstOrDefault();

            var lastRenter = context.Renters
                .OrderBy(r => r.Id)
                .LastOrDefault();

            if (firstVehicle != null && firstRenter != null)
            {
                context.Reservations.AddRange(
                    new Reservations { ReceivalDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(5), Status = "Valid", VehicleId = firstVehicle.Id, Vehicle = firstVehicle, RenterId = firstRenter.Id, Renter = firstRenter },
                    new Reservations { ReceivalDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(6), Status = "Valid", VehicleId = lastVehicle?.Id ?? 0, Vehicle = lastVehicle, RenterId = lastRenter?.Id ?? 0, Renter = lastRenter }
                );
                context.SaveChanges();
            }
        }
    }
}
