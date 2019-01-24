using System.ComponentModel.DataAnnotations;
using CarStore.Common;

namespace CarStore.Data.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinDepartmentName)]
        [MaxLength(GlobalConstants.MaxDepartmentName)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinDescriptionLength)]
        [MaxLength(GlobalConstants.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
