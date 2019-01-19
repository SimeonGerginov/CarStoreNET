using System.ComponentModel.DataAnnotations;
using CarStore.Common;
using Microsoft.AspNetCore.Identity;

namespace CarStore.Data.Models
{
    public class Customer : IdentityUser
    {
        [Required]
        [MinLength(GlobalConstants.MinNameLength)]
        [MaxLength(GlobalConstants.MaxNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinNameLength)]
        [MaxLength(GlobalConstants.MaxNameLength)]
        public string LastName { get; set; }

        [Required]
        [Range(GlobalConstants.MinAge, GlobalConstants.MaxAge)]
        public int Age { get; set; }
    }
}
