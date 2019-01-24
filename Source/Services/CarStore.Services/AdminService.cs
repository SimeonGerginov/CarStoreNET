using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task AddStoreCategoryToDbAsync(StoreCategory storeCategory, string departmentName)
        {
            var storeCategoryExists = this._carStoreDbContext.StoreCategories.Any(sc => sc.Name == storeCategory.Name);
            if (storeCategoryExists)
            {
                throw new InvalidOperationException("Store Category already exists.");
            }

            var department = this._carStoreDbContext.Departments.FirstOrDefault(d => d.Name == departmentName);
            if (department == null)
            {
                throw new InvalidOperationException("Department does not exist.");
            }

            storeCategory.DepartmentId = department.Id;

            await this._carStoreDbContext.StoreCategories.AddAsync(storeCategory);
            await this._carStoreDbContext.SaveChangesAsync();
        }

        public async Task AddDepartmentToDbAsync(Department department)
        {
            var departmentExists = this._carStoreDbContext.Departments.Any(d => d.Name == department.Name);

            if (departmentExists)
            {
                throw new InvalidOperationException("Department already exists.");
            }

            await this._carStoreDbContext.Departments.AddAsync(department);
            await this._carStoreDbContext.SaveChangesAsync();
        }

        public IEnumerable<Department> GetAllDepartmentsInDb()
        {
            return this._carStoreDbContext.Departments.AsEnumerable();
        }
    }
}
