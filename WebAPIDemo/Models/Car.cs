﻿using System.ComponentModel.DataAnnotations;
using WebAPIDemo.Models.Validations;

namespace WebAPIDemo.Models
{
    public class Car
    {
        [Required]
        public int CarId { get; set; }
        [Required]
        public string? CarBrand { get; set; }
        [Required]
        public string? CarName { get; set; }
        public string? CarDescription { get; set; }

        [Car_EnsureCorrectSeating]
        public int? CarSeat { get; set; }
        [Required]
        public string? CarType { get; set; }
        public string? CarColor { get; set; }
        public double? CarPrice { get; set; }

        public bool ValidateDescription()
        {
            return !string.IsNullOrEmpty(CarDescription);
        }

    }
}
