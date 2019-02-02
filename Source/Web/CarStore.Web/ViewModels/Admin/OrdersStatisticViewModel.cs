using System.Collections.Generic;
using CarStore.Web.ViewModels.Order;

namespace CarStore.Web.ViewModels.Admin
{
    public class OrdersStatisticViewModel
    {
        public OrdersStatisticViewModel()
        {
            this.Orders = new HashSet<OrderViewModel>();
        }

        public int Count { get; set; }

        public decimal TotalProfit { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; }
    }
}
