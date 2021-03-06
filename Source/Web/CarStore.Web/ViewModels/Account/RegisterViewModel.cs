﻿using System.ComponentModel.DataAnnotations;
using CarStore.Common;

namespace CarStore.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(
            GlobalConstants.MaxNameLength,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = GlobalConstants.MinNameLength)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(
            GlobalConstants.MaxNameLength,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = GlobalConstants.MinNameLength)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Range(
            GlobalConstants.MinNameLength, 
            GlobalConstants.MaxNameLength, 
            ErrorMessage = "The {0} must be at least {1} and at max {2} characters long.")]
        [Display(Name = "Години")]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required]
        [StringLength(
            GlobalConstants.MaxPasswordLength, 
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", 
            MinimumLength = GlobalConstants.MinPasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди Парола")]
        [Compare("Password", ErrorMessage = "The password and the confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
