using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Data.Seeding.Contracts;

namespace CarStore.Data.Seeding
{
    public class ModelSeeder : ISeeder
    {
        public async Task SeedAsync(CarStoreDbContext dbContext)
        {
            if (dbContext.Models.Any())
            {
                return;
            }

            var namesOfModels = new List<string>()
            {
                "California", "488 GTB", "488 Spider", "A4", "A3", "M3", "M4", "M6", "Lancia Dedra", "Lancia Delta",
                "Lancia Cappa"
            };
            
            var brandsIds = dbContext.Brands.Select(b => b.Id).ToList();
            var randomGeneratorForIds = new Random();

            foreach (var modelName in namesOfModels)
            {
                var brandId = randomGeneratorForIds.Next(1, brandsIds.Count - 1);
                await dbContext.Models.AddAsync(new Model { Name = modelName, BrandId = brandsIds[brandId] });
            }
        }
    }
}
