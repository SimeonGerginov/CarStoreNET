using System.Collections.Generic;
using CarStore.Data.Models;

namespace CarStore.Services.Contracts
{
    public interface ICatalogService
    {
        IEnumerable<Car> GetAllCarsFromDb();

        IEnumerable<Car> GetAllCarsInStoreCategoryFromDb(string storeCategoryName);

        byte[] GetCarImage(string carName);
    }
}
