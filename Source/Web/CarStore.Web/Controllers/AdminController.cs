using System.Linq;
using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Services.Contracts;
using CarStore.Web.ViewModels.Admin;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarStore.Web.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            this._adminService = adminService;
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
    }
}
