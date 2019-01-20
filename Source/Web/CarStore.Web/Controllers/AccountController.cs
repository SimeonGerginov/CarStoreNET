using System.Threading.Tasks;

using CarStore.Common;
using CarStore.Data.Models;
using CarStore.Web.ViewModels.Account;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login()
        {
            // Clears the existing external cookie to ensure a clean login process.
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this._signInManager.PasswordSignInAsync(model.Email, model.Password,
                    isPersistent: true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return this.RedirectToAction(nameof(HomeController.Index), "Home");
                }

                // If the login was not successful.
                this.ModelState.TryAddModelError(GlobalConstants.LoginErrorKey, GlobalConstants.LoginErrorMessage);
            }

            // If we got this far, something failed, redisplay form.
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return this.RedirectToAction(nameof(HomeController.Index), "Home");
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
                    await this._signInManager.SignInAsync(customer, isPersistent: true);
                    return this.RedirectToAction(nameof(HomeController.Index), "Home");
                }

                // If the creation of the Customer was not successful.
                this.AddErrorsToModelState(result);
            }

            // If we got this far, something failed, redisplay form.
            return this.View(model);
        }
    }
}
