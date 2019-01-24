using System.Collections.Generic;
using CarStore.Data.Models;

namespace CarStore.Services.Contracts
{
    public interface ICatalogService
    {
        IEnumerable<Car> GetAllCarsFromDb();

        byte[] GetCarImage(string carName);
    }
}
