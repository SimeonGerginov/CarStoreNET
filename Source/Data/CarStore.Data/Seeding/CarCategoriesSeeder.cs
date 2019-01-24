using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Data.Seeding.Contracts;

namespace CarStore.Data.Seeding
{
    public class CarCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(CarStoreDbContext dbContext)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var namesOfCategories = new List<string>()
            {
                "Van", "Jeep", "Convertible", "Combi", "Coupe", "Minivan", "Pickup"
            };

            foreach (var categoryName in namesOfCategories)
            {
                await dbContext.Categories.AddAsync(new Category { Name = categoryName });
            }
        }
    }
}
