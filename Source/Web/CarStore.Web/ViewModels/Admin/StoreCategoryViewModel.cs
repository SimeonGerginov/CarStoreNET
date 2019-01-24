using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CarStore.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarStore.Web.ViewModels.Admin
{
    public class StoreCategoryViewModel
    {
        public IEnumerable<SelectListItem> DepartmentsSelectList { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCategoryName)]
        [MaxLength(GlobalConstants.MaxCategoryName)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinDescriptionLength)]
        [MaxLength(GlobalConstants.MaxDescriptionLength)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Отдел")]
        public string DepartmentName { get; set; }
    }
}
