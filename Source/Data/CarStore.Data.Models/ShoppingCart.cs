using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CarStore.Data.Models.Enums;

namespace CarStore.Data.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        [Key]
        public int Id { get; set; }
        
        public string CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public Status Status { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
