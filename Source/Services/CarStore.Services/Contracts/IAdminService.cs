using System.Collections.Generic;
using System.Threading.Tasks;

using CarStore.Data.Models;

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
    }
}
