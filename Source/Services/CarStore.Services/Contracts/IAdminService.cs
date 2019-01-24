using System.Collections.Generic;
using System.Threading.Tasks;

using CarStore.Data.Models;

namespace CarStore.Services.Contracts
{
    public interface IAdminService
    {
        void AddCarToDb(Car car);

        Task AddStoreCategoryToDbAsync(StoreCategory storeCategory, string departmentName);

        Task AddDepartmentToDbAsync(Department department);

        IEnumerable<Department> GetAllDepartmentsInDb();
    }
}
