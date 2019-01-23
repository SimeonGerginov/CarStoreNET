using System;
using System.Linq;

using CarStore.Data;
using CarStore.Data.Models;
using CarStore.Services.Contracts;

namespace CarStore.Services
{
    public class AdminService : IAdminService
    {
        private readonly CarStoreDbContext _carStoreDbContext;

        public AdminService(CarStoreDbContext carStoreDbContext)
        {
            this._carStoreDbContext = carStoreDbContext;
        }

        public void AddCarToDb(Car car)
        {
            var carExists = this._carStoreDbContext.Cars
                .Any(c => c.Name == car.Name && c.BrandId == car.BrandId && c.ModelId == car.ModelId);

            if (carExists)
            {
                throw new InvalidOperationException("Car already exists.");
            }

            this._carStoreDbContext.Cars.Add(car);
            this._carStoreDbContext.SaveChanges();
        }

        public void AddStoreCategoryToDb(StoreCategory storeCategory)
        {
            var storeCategoryExists = this._carStoreDbContext.StoreCategories.Any(sc => sc.Name == storeCategory.Name);

            if (storeCategoryExists)
            {
                throw new InvalidOperationException("Store Category already exists.");
            }

            this._carStoreDbContext.StoreCategories.Add(storeCategory);
            this._carStoreDbContext.SaveChanges();
        }

        public void AddDepartmentToDb(Department department)
        {
            var departmentExists = this._carStoreDbContext.Departments.Any(d => d.Name == department.Name);

            if (departmentExists)
            {
                throw new InvalidOperationException("Department already exists.");
            }

            this._carStoreDbContext.Departments.Add(department);
            this._carStoreDbContext.SaveChanges();
        }
    }
}
