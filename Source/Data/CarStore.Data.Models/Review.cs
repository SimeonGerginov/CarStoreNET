using System;
using System.ComponentModel.DataAnnotations;

using CarStore.Common;

namespace CarStore.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinReviewLength)]
        [MaxLength(GlobalConstants.MaxReviewLength)]
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
