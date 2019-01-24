namespace CarStore.Data.Models
{
    public class CarStoreCategory
    {
        public int CarId { get; set; }

        public Car Car { get; set; }

        public int StoreCategoryId { get; set; }

        public StoreCategory StoreCategory { get; set; }
    }
}
