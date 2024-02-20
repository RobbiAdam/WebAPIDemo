using System.ComponentModel.DataAnnotations;
using WebApp.Models.Validations;

namespace WebApp.Models
{
    public class Car
    {
        [Required]
        public int CarId { get; set; }
        [Required]
        public string? CarBrand { get; set; }
        [Required]
        public string? CarName { get; set; }

        [Car_EnsureCorrectSeating]
        public int? CarSeat { get; set; }
        [Required]
        public string? CarType { get; set; }

        public string? CarColor { get; set; }
        [Required]
        public double? CarPrice { get; set; }

    }
}
