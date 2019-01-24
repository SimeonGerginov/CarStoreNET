using System;
using System.Collections.Generic;
using CarStore.Common;
using CarStore.Data.Models;
using CarStore.Services.Contracts;
using CarStore.Web.ViewModels.Catalog;

using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CarStore.Web.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this._catalogService = catalogService;
        }

        public IActionResult Index(string searchString, int? page)
        {
            this.ViewData["CurrentFilter"] = searchString;
            
            var cars = !string.IsNullOrEmpty(searchString) 
                ? this._catalogService.GetAllCarsInStoreCategoryFromDb(searchString) 
                : this._catalogService.GetAllCarsFromDb();
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

                catalogCars.Add(catalogCar);
            }

            var pageNumber = page ?? 1;
            var carsPerPage = catalogCars.ToPagedList(pageNumber, GlobalConstants.CarsPerPage);

            return this.View(carsPerPage);
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
