using CarStore.Data.Models;

namespace CarStore.Services.Contracts
{
    public interface IAdminService
    {
        void AddCarToDb(Car car);

        void AddStoreCategoryToDb(StoreCategory storeCategory);

        void AddDepartmentToDb(Department department);
    }
}
