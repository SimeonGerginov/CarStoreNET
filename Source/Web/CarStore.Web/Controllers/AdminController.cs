using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Services.Contracts;
using CarStore.Web.ViewModels.Admin;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
