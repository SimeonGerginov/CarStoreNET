using System;
using System.Collections.Generic;

using CarStore.Web.ViewModels.ShoppingCart;

namespace CarStore.Web.ViewModels.Order
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            this.CarItems = new HashSet<CarItemViewModel>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DateAdded { get; set; }

        public ICollection<CarItemViewModel> CarItems { get; set; }
    }
}
