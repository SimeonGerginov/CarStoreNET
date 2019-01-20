using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected void AddErrorsToModelState(IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
            {
                this.ModelState.TryAddModelError(error.Code, error.Description);
            }
        }
    }
}
