using System.Collections.Generic;

namespace CarStore.Web.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            this.CarItems = new HashSet<CarItemViewModel>();
        }

        public int ShoppingCartId { get; set; }

        public ICollection<CarItemViewModel> CarItems { get; set; }
    }
}
