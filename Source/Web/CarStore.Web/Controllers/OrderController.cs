using System.Collections.Generic;
using System.Threading.Tasks;

using CarStore.Services.Contracts;
using CarStore.Web.ViewModels.Order;
using CarStore.Web.ViewModels.ShoppingCart;

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

        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var orders = await this._orderService.GetAllOrdersOfCustomer(this.User);
            var ordersViewModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var orderViewModel = new OrderViewModel
                {
                    FirstName = order.Customer.FirstName,
                    LastName = order.Customer.LastName,
                    TotalPrice = order.TotalPrice,
                    DateAdded = order.DateAdded
                };

                foreach (var shoppingCartItem in order.ShoppingCart.ShoppingCartItems)
                {
                    var carItemViewModel = new CarItemViewModel
                    {
                        CarId = shoppingCartItem.CarId,
                        CarName = shoppingCartItem.Car.Name,
                        Quantity = shoppingCartItem.Quantity,
                        TotalPrice = shoppingCartItem.Car.Price * shoppingCartItem.Quantity
                    };

                    orderViewModel.CarItems.Add(carItemViewModel);
                }

                ordersViewModel.Add(orderViewModel);
            }

            return this.View(ordersViewModel);
        }
    }
}
