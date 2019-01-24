using System.ComponentModel.DataAnnotations;
using CarStore.Common;

namespace CarStore.Data.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinBrandName)]
        [MaxLength(GlobalConstants.MaxBrandName)]
        public string Name { get; set; }
    }
}
