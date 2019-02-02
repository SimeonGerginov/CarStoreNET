using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarStore.Data;
using CarStore.Data.Models;
using CarStore.Data.Models.Enums;
using CarStore.Services.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CarStore.Services
{
    public class AdminService : IAdminService
    {
        private readonly CarStoreDbContext _carStoreDbContext;

        public AdminService(CarStoreDbContext carStoreDbContext)
        {
            this._carStoreDbContext = carStoreDbContext;
        }

        public async Task AddCarToDbAsync(Car car, string brandName, string modelName, string categoryName, string storeCategoryName)
        {
            var carExists = this._carStoreDbContext.Cars
                .Any(c => c.Name == car.Name && c.Price == car.Price && c.Description == car.Description);

            if (carExists)
            {
                throw new InvalidOperationException("Car already exists.");
            }

            var brand = this._carStoreDbContext.Brands.FirstOrDefault(b => b.Name == brandName);
            if (brand == null)
            {
                throw new InvalidOperationException("Brand does not exist.");
            }

            var model = this._carStoreDbContext.Models.FirstOrDefault(m => m.Name == modelName);
            if (model == null)
            {
                throw new InvalidOperationException("Model does not exist.");
            }

            var category = this._carStoreDbContext.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                throw new InvalidOperationException("Category does not exist.");
            }

            var storeCategory = this._carStoreDbContext.StoreCategories.FirstOrDefault(sc => sc.Name == storeCategoryName);
            if (storeCategory == null)
            {
                throw new InvalidOperationException("Store Category does not exist.");
            }

            car.BrandId = brand.Id;
            car.ModelId = model.Id;

            var carCategory = new CarCategory
            {
                Car = car,
                Category = category
            };

            var carStoreCategory = new CarStoreCategory
            {
                Car = car,
                StoreCategory = storeCategory
            };

            car.CarCategories.Add(carCategory);
            car.CarStoreCategories.Add(carStoreCategory);
            
            await this._carStoreDbContext.Cars.AddAsync(car);
            await this._carStoreDbContext.SaveChangesAsync();
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

        public IEnumerable<Brand> GetAllBrandsInDb()
        {
            return this._carStoreDbContext.Brands.AsEnumerable();
        }

        public IEnumerable<Model> GetAllModelsInDb()
        {
            return this._carStoreDbContext.Models.AsEnumerable();
        }

        public IEnumerable<Department> GetAllDepartmentsInDb()
        {
            return this._carStoreDbContext.Departments.AsEnumerable();
        }

        public IEnumerable<StoreCategory> GetAllStoreCategoriesInDb()
        {
            return this._carStoreDbContext.StoreCategories.AsEnumerable();
        }

        public IEnumerable<Category> GetAllCategoriesInDb()
        {
            return this._carStoreDbContext.Categories.AsEnumerable();
        }

        public IEnumerable<Order> GetNotProcessedOrders()
        {
            return this._carStoreDbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.ShoppingCart)
                .ThenInclude(sc => sc.ShoppingCartItems)
                .ThenInclude(sc => sc.Car)
                .Where(o => o.Status == OrderStatus.NotProcessed)
                .AsEnumerable();
        }
    }
}
