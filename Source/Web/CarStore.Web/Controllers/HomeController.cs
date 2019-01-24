using System.Diagnostics;
using CarStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.RedirectToAction(nameof(CatalogController.Index), "Catalog");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
