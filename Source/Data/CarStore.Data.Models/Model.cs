using System.ComponentModel.DataAnnotations;
using CarStore.Common;

namespace CarStore.Data.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinModelName)]
        [MaxLength(GlobalConstants.MaxModelName)]
        public string Name { get; set; }
        
        public int? BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}
