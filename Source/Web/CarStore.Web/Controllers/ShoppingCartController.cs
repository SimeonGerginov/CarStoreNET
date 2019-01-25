using System;
using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Services.Contracts;
using CarStore.Web.ViewModels.ShoppingCart;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this._shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> ShoppingCart()
        {
            var shoppingCart = await this._shoppingCartService.GetShoppingCart(this.User);
            var shoppingCartViewModel = new ShoppingCartViewModel();

            if (shoppingCart != null)
            {
                shoppingCartViewModel.ShoppingCartId = shoppingCart.Id;
                foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
                {
                    var carItemViewModel = new CarItemViewModel
                    {
                        CarId = shoppingCartItem.Car.Id,
                        CarName = shoppingCartItem.Car.Name,
                        Quantity = shoppingCartItem.Quantity,
                        TotalPrice = shoppingCartItem.Car.Price * shoppingCartItem.Quantity
                    };

                    shoppingCartViewModel.CarItems.Add(carItemViewModel);
                }
            }

            return this.View(shoppingCartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart(int carId)
        {
            var shoppingCartItem = new ShoppingCartItem
            {
                CarId = carId,
                Quantity = 1,
                DateAdded = DateTime.UtcNow
            };

            await this._shoppingCartService.AddItemToShoppingCart(this.User, shoppingCartItem);
            return this.RedirectToAction(nameof(ShoppingCartController.ShoppingCart), "ShoppingCart");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantityOfShoppingCartItem(int carId, decimal quantity)
        {
            await this._shoppingCartService.UpdateQuantityOfShoppingCartItem(this.User, carId, quantity);
            return this.RedirectToAction(nameof(ShoppingCartController.ShoppingCart), "ShoppingCart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItemFromShoppingCart(int carId)
        {
            await this._shoppingCartService.RemoveItemFromShoppingCart(this.User, carId);
            return this.RedirectToAction(nameof(ShoppingCartController.ShoppingCart), "ShoppingCart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveShoppingCart(int shoppingCartId)
        {
            await this._shoppingCartService.RemoveShoppingCart(this.User, shoppingCartId);
            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
