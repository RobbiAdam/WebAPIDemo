namespace WebAPIDemo.Models.Repositories
{
    public static class CarRepository
    {
        private static List<Car> cars = new List<Car>()
        {
            new Car { CarId = 1, CarBrand = "Toyota", CarName = "Camry", CarSeat = 4, CarType = "Sedan", CarColor = "Silver", CarPrice = 25000 },
            new Car { CarId = 2, CarBrand = "Honda", CarName = "Civic", CarSeat = 4, CarType = "Sedan", CarColor = "Red", CarPrice = 23000 },
            new Car { CarId = 3, CarBrand = "Ford", CarName = "F-150", CarSeat = 2, CarType = "Sport", CarColor = "Blue", CarPrice = 35000 },
            new Car { CarId = 4, CarBrand = "Chevrolet", CarName = "Camaro", CarSeat = 2, CarType = "Sport", CarColor = "Black", CarPrice = 45000 }
        };

        public static List<Car> GetCars()
        {
            return cars;
        }
        public static bool CarExist(int id)
        {
            return cars.Any(x => x.CarId == id);
        }

        public static Car? GetCarById(int id)
        {
            return cars.FirstOrDefault(x => x.CarId == id);
        }

        public static Car? GetCarByProperties(string? carBrand, string? carName, int? carSeat, string? carType, string? carColor)
        {
            return cars.FirstOrDefault(x =>
                !string.IsNullOrWhiteSpace(carBrand)
                && !string.IsNullOrWhiteSpace(x.CarBrand) &&
                x.CarBrand.Equals(carBrand, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(carName)
                && !string.IsNullOrWhiteSpace(x.CarName) &&
                x.CarName.Equals(carName, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(carColor)
                && !string.IsNullOrWhiteSpace(x.CarColor) &&
                x.CarColor.Equals(carColor, StringComparison.OrdinalIgnoreCase) &&
                carSeat.HasValue &&
                carSeat.Value == x.CarSeat.Value);
        }
        public static void AddCar(Car car)
        {
            int maxId = cars.Max(x => x.CarId);
            car.CarId = maxId + 1;

            cars.Add(car);
        }
        public static void UpdateCar(Car car)
        {
            var carToUpdate = cars.First(x => x.CarId == car.CarId);

            carToUpdate.CarBrand = car.CarBrand;
            carToUpdate.CarName = car.CarName;
            carToUpdate.CarSeat = car.CarSeat;
            carToUpdate.CarType = car.CarType;
            carToUpdate.CarColor = car.CarColor;
            carToUpdate.CarPrice = car.CarPrice;
        }

        public static void DeleteCar(int carId)
        {
            var car = GetCarById(carId);

            if (car != null)
            {
                cars.Remove(car);
            }
        }
    }
}
