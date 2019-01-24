using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Web.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        [HttpGet]
        public IActionResult AddCar()
        {
            return this.View();
        }
    }
}
