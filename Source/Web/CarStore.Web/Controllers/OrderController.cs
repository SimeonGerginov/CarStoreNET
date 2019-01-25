using System.Threading.Tasks;
using CarStore.Services.Contracts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int shoppingCartId)
        {
            await this._orderService.CreateOrder(this.User, shoppingCartId);
            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
