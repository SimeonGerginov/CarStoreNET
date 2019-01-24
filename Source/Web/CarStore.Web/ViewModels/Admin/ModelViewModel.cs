using System.ComponentModel.DataAnnotations;
using CarStore.Common;

namespace CarStore.Web.ViewModels.Admin
{
    public class ModelViewModel
    {
        [Required]
        [MinLength(GlobalConstants.MinModelName)]
        [MaxLength(GlobalConstants.MaxModelName)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinBrandName)]
        [MaxLength(GlobalConstants.MaxBrandName)]
        public string BrandName { get; set; }
    }
}
