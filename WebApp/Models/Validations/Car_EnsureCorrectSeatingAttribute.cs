using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Validations
{
    public class Car_EnsureCorrectSeatingAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var car = validationContext.ObjectInstance as Car;

            if (car != null)
            {
                if (car.CarType.Equals("sport", StringComparison.OrdinalIgnoreCase) && car.CarSeat != 2)
                {
                    return new ValidationResult("For sport car, seat must equal to 2");
                }

                else if (car.CarType.Equals("sedan", StringComparison.OrdinalIgnoreCase) && car.CarSeat < 2 || car.CarSeat > 6)

                {
                    return new ValidationResult("For sedan car, seat must more than 2 and less than 6");
                }

            }
            return ValidationResult.Success;
        }
    }
}
