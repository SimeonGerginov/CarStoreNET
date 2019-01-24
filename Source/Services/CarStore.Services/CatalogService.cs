using System;
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
                .AsEnumerable();
        }

        public IEnumerable<Car> GetAllCarsInStoreCategoryFromDb(string storeCategoryName)
        {
            var storeCategoryExists = this._carStoreDbContext.StoreCategories.Any(sc => sc.Name == storeCategoryName);

            if (!storeCategoryExists)
            {
                throw new InvalidOperationException("The Store Category does not exist.");
            }

            var storeCategory = this._carStoreDbContext.StoreCategories
                .Where(sc => sc.Name == storeCategoryName)
                .Include(sc => sc.CarStoreCategories)
                .ThenInclude(scc => scc.Car)
                .ThenInclude(c => c.Brand)
                .Include(sc => sc.CarStoreCategories)
                .ThenInclude(scc => scc.Car)
                .ThenInclude(c => c.Model)
                .First();

            var carsInStoreCategory = new List<Car>();

            foreach (var carStoreCategory in storeCategory.CarStoreCategories)
            {
                carsInStoreCategory.Add(carStoreCategory.Car);
            }

            return carsInStoreCategory;
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
