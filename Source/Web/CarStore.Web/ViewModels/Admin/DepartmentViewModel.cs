using System.ComponentModel.DataAnnotations;
using CarStore.Common;

namespace CarStore.Web.ViewModels.Admin
{
    public class DepartmentViewModel
    {
        [Required]
        [MinLength(GlobalConstants.MinDepartmentName)]
        [MaxLength(GlobalConstants.MaxDepartmentName)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinDescriptionLength)]
        [MaxLength(GlobalConstants.MaxDescriptionLength)]
        [Display(Name = "Oписание")]
        public string Description { get; set; }
    }
}
