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
                "California", "488 GTB", "488 Spider"
            };

            // TODO: This should be refactored to check if the brand Ferrari exists in the DB.
            var ferraryId = dbContext.Brands.First(b => b.Name == "Ferrari").Id;

            foreach (var modelName in namesOfModels)
            {
                await dbContext.Models.AddAsync(new Model { Name = modelName, BrandId = ferraryId });
            }
        }
    }
}
