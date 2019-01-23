using System.Threading.Tasks;

namespace CarStore.Data.Seeding.Contracts
{
    public interface ISeeder
    {
        Task SeedAsync(CarStoreDbContext dbContext);
    }
}
