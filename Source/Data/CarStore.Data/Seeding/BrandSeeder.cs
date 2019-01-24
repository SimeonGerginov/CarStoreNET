using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Data.Seeding.Contracts;

namespace CarStore.Data.Seeding
{
    public class BrandSeeder : ISeeder
    {
        public async Task SeedAsync(CarStoreDbContext dbContext)
        {
            if (dbContext.Brands.Any())
            {
                return;
            }

            var namesOfBrands = new List<string>()
            {
                "Lancia", "Ferrari", "Fiat", "Audi", "Mercedes", "BMW", "Peugeot", "Citroen", "Renault",
                "Lada", "Volga", "Toyota", "Honda", "Ford", "Chevrolet"
            };

            foreach (var brandName in namesOfBrands)
            {
                await dbContext.Brands.AddAsync(new Brand { Name = brandName });
            }
        }
    }
}
