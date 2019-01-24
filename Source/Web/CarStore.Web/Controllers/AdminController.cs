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
            return this.View();
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
