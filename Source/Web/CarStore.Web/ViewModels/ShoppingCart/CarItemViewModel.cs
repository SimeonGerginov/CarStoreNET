namespace CarStore.Web.ViewModels.ShoppingCart
{
    public class CarItemViewModel
    {
        public int CarId { get; set; }

        public string CarName { get; set; }

        public decimal Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
