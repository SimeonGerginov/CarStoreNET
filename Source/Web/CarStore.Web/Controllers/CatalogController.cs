using System;
using System.Collections.Generic;
using System.Globalization;
using CarStore.Services.Contracts;
using CarStore.Web.ViewModels.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Web.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this._catalogService = catalogService;
        }

        public IActionResult Index()
        {
            var cars = this._catalogService.GetAllCarsFromDb();
            var catalogCars = new List<CatalogCarViewModel>();

            foreach (var car in cars)
            {
                var catalogCar = new CatalogCarViewModel()
                {
                    Name = car.Name,
                    Description = car.Description,
                    Price = car.Price,
                    BrandName = car.Brand.Name,
                    ModelName = car.Model.Name,
                    YearOfManufacture = car.YearOfManufacture,
                    Color = car.Color,
                    Mileage = car.Mileage,
                    Gearbox = car.Gearbox,
                    EngineType = car.EngineType
                };

                foreach (var carCategory in car.CarCategories)
                {
                    catalogCar.CategoriesNames.Add(carCategory.Category.Name);
                }

                foreach (var carStoreCategory in car.CarStoreCategories)
                {
                    catalogCar.StoreCategoriesNames.Add(carStoreCategory.StoreCategory.Name);
                }

                catalogCars.Add(catalogCar);
            }

            return this.View(catalogCars);
        }

        public FileContentResult GetCarPhoto(string carName)
        {
            var carImage = this._catalogService.GetCarImage(carName);

            if (carImage == null)
            {
                throw new InvalidOperationException("The car image was not found.");
            }

            var image = this.File(carImage, "image/png");

            return image;
        }
    }
}
