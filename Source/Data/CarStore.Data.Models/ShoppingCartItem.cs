using System;
using System.ComponentModel.DataAnnotations;

namespace CarStore.Data.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
