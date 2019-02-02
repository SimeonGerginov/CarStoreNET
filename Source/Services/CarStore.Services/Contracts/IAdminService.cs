using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Data.Models.Enums;

namespace CarStore.Services.Contracts
{
    public interface IAdminService
    {
        Task AddCarToDbAsync(Car car, string brandName, string modelName, string categoryName, string storeCategoryName);

        Task AddStoreCategoryToDbAsync(StoreCategory storeCategory, string departmentName);

        Task AddDepartmentToDbAsync(Department department);

        IEnumerable<Brand> GetAllBrandsInDb();

        IEnumerable<Model> GetAllModelsInDb();

        IEnumerable<Department> GetAllDepartmentsInDb();

        IEnumerable<StoreCategory> GetAllStoreCategoriesInDb();

        IEnumerable<Category> GetAllCategoriesInDb();

        IEnumerable<Order> GetNotProcessedOrders();

        Task UpdateOrderStatus(int orderId, OrderStatus status);

        IEnumerable<Order> GetApprovedOrdersInInterval(DateTime startDate, DateTime endDate);
    }
}
