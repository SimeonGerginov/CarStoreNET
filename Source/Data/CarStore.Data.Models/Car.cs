using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CarStore.Common;
using CarStore.Data.Models.Enums;

namespace CarStore.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.CarCategories = new HashSet<CarCategory>();
            this.CarStoreCategories = new HashSet<CarStoreCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCarName)]
        [MaxLength(GlobalConstants.MaxCarName)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinDescriptionLength)]
        [MaxLength(GlobalConstants.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public byte[] Image { get; set; }
        
        public int?  BrandId { get; set; }

        public Brand Brand { get; set; }
        
        public int? ModelId { get; set; }

        public Model Model { get; set; }

        [Required]
        [Range(GlobalConstants.MinYearLength, GlobalConstants.MaxYearLength)]
        public int YearOfManufacture { get; set; }

        [Required]
        public Color Color { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public Gearbox Gearbox { get; set; }

        [Required]
        public EngineType EngineType { get; set; }

        public ICollection<CarCategory> CarCategories { get; set; }

        public ICollection<CarStoreCategory> CarStoreCategories { get; set; }
    }
}
