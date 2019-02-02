using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarStore.Common;
using CarStore.Data.Models;
using CarStore.Data.Models.Enums;
using CarStore.Services.Contracts;
using CarStore.Web.ViewModels.Admin;
using CarStore.Web.ViewModels.Order;
using CarStore.Web.ViewModels.ShoppingCart;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarStore.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly IFileConverter _fileConverter;

        public AdminController(IAdminService adminService, IFileConverter fileConverter)
        {
            this._adminService = adminService;
            this._fileConverter = fileConverter;
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            var brandsSelectList = this._adminService
                .GetAllBrandsInDb()
                .Select(b => new SelectListItem { Text = b.Name, Value = b.Name });

            var modelsSelectList = this._adminService
                .GetAllModelsInDb()
                .Select(m => new SelectListItem { Text = m.Name, Value = m.Name });

            var categoriesSelectList = this._adminService
                .GetAllCategoriesInDb()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Name });

            var storeCategoriesSelectList = this._adminService
                .GetAllStoreCategoriesInDb()
                .Select(sc => new SelectListItem { Text = sc.Name, Value = sc.Name });

            var carViewModel = new CarViewModel
            {
                BrandsSelectList = brandsSelectList,
                ModelsSelectList = modelsSelectList,
                CategoriesSelectList = categoriesSelectList,
                StoreCategoriesSelectList = storeCategoriesSelectList
            };

            return this.View(carViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCar(CarViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var image = await this._fileConverter.PostedToByteArray(model.Image);

                var car = new Car
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Image = image,
                    YearOfManufacture = model.YearOfManufacture,
                    Color = model.Color,
                    Mileage = model.Mileage,
                    Gearbox = model.Gearbox,
                    EngineType = model.EngineType
                };

                var brandName = model.BrandName;
                var modelName = model.ModelName;
                var categoryName = model.CategoryName;
                var storeCategoryName = model.StoreCategoryName;

                await this._adminService.AddCarToDbAsync(car, brandName, modelName, categoryName, storeCategoryName);
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }

            // If we got this far, something failed, redisplay form.
            return this.View(model);
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(DepartmentViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var department = new Department
                {
                    Name = model.Name,
                    Description = model.Description
                };

                await this._adminService.AddDepartmentToDbAsync(department);
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }

            // If we got this far, something failed, redisplay form.
            return this.View(model);
        }

        [HttpGet]
        public IActionResult AddStoreCategory()
        {
            var departmentsSelectList = this._adminService
                .GetAllDepartmentsInDb()
                .Select(d => new SelectListItem { Text = d.Name, Value = d.Name });
            
            var storeCategoryViewModel = new StoreCategoryViewModel
            {
                DepartmentsSelectList = departmentsSelectList
            };

            return this.View(storeCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStoreCategory(StoreCategoryViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var storeCategory = new StoreCategory
                {
                    Name = model.Name,
                    Description = model.Description
                };

                await this._adminService.AddStoreCategoryToDbAsync(storeCategory, model.DepartmentName);
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }

            // If we got this far, something failed, redisplay form.
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var orders = this._adminService.GetNotProcessedOrders();
            var ordersViewModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var orderViewModel = new OrderViewModel
                {
                    FirstName = order.Customer.FirstName,
                    LastName = order.Customer.LastName,
                    TotalPrice = order.TotalPrice,
                    DateAdded = order.DateAdded
                };

                foreach (var shoppingCartItem in order.ShoppingCart.ShoppingCartItems)
                {
                    var carItemViewModel = new CarItemViewModel
                    {
                        CarId = shoppingCartItem.CarId,
                        CarName = shoppingCartItem.Car.Name,
                        Quantity = shoppingCartItem.Quantity,
                        TotalPrice = shoppingCartItem.Car.Price * shoppingCartItem.Quantity
                    };

                    orderViewModel.CarItems.Add(carItemViewModel);
                }

                ordersViewModel.Add(orderViewModel);
            }

            return this.View(ordersViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeOrderStatus(OrderStatus orderStatus)
        {
            return this.RedirectToAction(nameof(AdminController.Orders), "Admin");
        }
    }
}
