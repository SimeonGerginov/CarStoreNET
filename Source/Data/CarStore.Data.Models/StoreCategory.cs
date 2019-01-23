using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CarStore.Common;

namespace CarStore.Data.Models
{
    public class StoreCategory
    {
        public StoreCategory()
        {
            this.CarStoreCategories = new HashSet<CarStoreCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCategoryName)]
        [MaxLength(GlobalConstants.MaxCategoryName)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinDescriptionLength)]
        [MaxLength(GlobalConstants.MaxDescriptionLength)]
        public string Description { get; set; }
        
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<CarStoreCategory> CarStoreCategories { get; set; }
    }
}
