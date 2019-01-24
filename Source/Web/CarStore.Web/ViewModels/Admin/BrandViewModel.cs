using System.ComponentModel.DataAnnotations;
using CarStore.Common;

namespace CarStore.Web.ViewModels.Admin
{
    public class BrandViewModel
    {
        [Required]
        [MinLength(GlobalConstants.MinBrandName)]
        [MaxLength(GlobalConstants.MaxBrandName)]
        public string Name { get; set; }
    }
}
