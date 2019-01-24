using System.Collections.Generic;
using System.Linq;

using CarStore.Data;
using CarStore.Data.Models;
using CarStore.Services.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CarStore.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly CarStoreDbContext _carStoreDbContext;

        public CatalogService(CarStoreDbContext carStoreDbContext)
        {
            this._carStoreDbContext = carStoreDbContext;
        }

        public IEnumerable<Car> GetAllCarsFromDb()
        {
            return this._carStoreDbContext.Cars
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .Include(c => c.CarCategories)
                .ThenInclude(cc => cc.Category)
                .Include(c => c.CarStoreCategories)
                .ThenInclude(csc => csc.StoreCategory)
                .AsEnumerable();
        }

        public byte[] GetCarImage(string carName)
        {
            return this._carStoreDbContext.Cars
                .Where(c => c.Name == carName)
                .Select(c => c.Image)
                .FirstOrDefault();
        }
    }
}
