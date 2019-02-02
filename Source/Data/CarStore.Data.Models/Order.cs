using System;
using System.ComponentModel.DataAnnotations;

using CarStore.Data.Models.Enums;

namespace CarStore.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
        
        public string CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        public OrderStatus Status { get; set; }
    }
}
