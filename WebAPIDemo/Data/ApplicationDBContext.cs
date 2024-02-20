using Microsoft.EntityFrameworkCore;
using WebAPIDemo.Models;

namespace WebAPIDemo.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Car>().HasData(
            new Car { CarId = 1, CarBrand = "Toyota", CarName = "Camry", CarSeat = 4, CarType = "Sedan", CarColor = "Silver", CarPrice = 25000 },
            new Car { CarId = 2, CarBrand = "Honda", CarName = "Civic", CarSeat = 4, CarType = "Sedan", CarColor = "Red", CarPrice = 23000 },
            new Car { CarId = 3, CarBrand = "Ford", CarName = "F-150", CarSeat = 2, CarType = "Sport", CarColor = "Blue", CarPrice = 35000 },
            new Car { CarId = 4, CarBrand = "Chevrolet", CarName = "Camaro", CarSeat = 2, CarType = "Sport", CarColor = "Black", CarPrice = 45000 }

            );

        }
    }
}
