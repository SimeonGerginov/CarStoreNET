using System;
using System.Threading.Tasks;

using CarStore.Data.Models;
using CarStore.Services.Contracts;

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
        public async Task<IActionResult> GetShoppingCart()
        {
            var shoppingCart = await this._shoppingCartService.GetShoppingCart(this.User);
            return this.View(shoppingCart);
        }

        [HttpPut]
        public async Task<IActionResult> AddItemToShoppingCart(int carId)
        {
            var shoppingCartItem = new ShoppingCartItem
            {
                CarId = carId,
                Quantity = 1,
                DateAdded = DateTime.UtcNow
            };

            await this._shoppingCartService.AddItemToShoppingCart(this.User, shoppingCartItem);
            return this.Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuantityOfShoppingCartItem(int carId, decimal quantity)
        {
            await this._shoppingCartService.UpdateQuantityOfShoppingCartItem(this.User, carId, quantity);
            return this.Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItemFromShoppingCart(int carId)
        {
            await this._shoppingCartService.RemoveItemFromShoppingCart(this.User, carId);
            return this.Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveShoppingCart()
        {
            await this._shoppingCartService.RemoveShoppingCart(this.User);
            return this.Ok();
        }
    }
}
