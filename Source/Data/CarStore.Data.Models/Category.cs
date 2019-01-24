using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CarStore.Common;

namespace CarStore.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.CarCategories = new HashSet<CarCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCategoryName)]
        [MaxLength(GlobalConstants.MaxCategoryName)]
        public string Name { get; set; }

        public ICollection<CarCategory> CarCategories { get; set; }
    }
}
