using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Web.Infrastructure;
using CarStore.Web.ViewModels.Account;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;

        public AccountController(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;

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
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, this.ModelState));
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
    }
}
