namespace CarStore.Data.Models
{
    public class CarCategory
    {
        public int CarId { get; set; }

        public Car Car { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
