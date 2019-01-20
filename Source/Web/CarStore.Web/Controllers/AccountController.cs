using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Web.ViewModels.Account;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;

        public AccountController(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var customer = new Customer
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age
                };

                var result = await this._userManager.CreateAsync(customer, model.Password);
                if (result.Succeeded)
                {
                    await this._signInManager.SignInAsync(customer, isPersistent: false);
                    return this.RedirectToAction(nameof(HomeController.Index), "Home");
                }

                // If the creation of the Customer was not successful.
                this.AddErrorsToModelState(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
    }
}
