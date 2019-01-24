using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CarStore.Data.Seeding.Contracts;

namespace CarStore.Data.Seeding
{
    public class CarStoreDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(CarStoreDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            var seeders = new List<ISeeder>
            {
                new BrandSeeder(),
                new ModelSeeder(),
                new CarCategoriesSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
