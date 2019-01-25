using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using CarStore.Data;
using CarStore.Data.Models;
using CarStore.Data.Models.Enums;
using CarStore.Services.Contracts;

using Microsoft.AspNetCore.Identity;

namespace CarStore.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly CarStoreDbContext _carStoreDbContext;

        public ShoppingCartService(UserManager<Customer> userManager, CarStoreDbContext carStoreDbContext)
        {
            this._userManager = userManager;
            this._carStoreDbContext = carStoreDbContext;
        }

        public async Task CreateShoppingCart(ClaimsPrincipal currentUser)
        {
            var customer = await this._userManager.GetUserAsync(currentUser);
            var shoppingCart = new ShoppingCart()
            {
                Customer = customer,
                Status = Status.Created
            };

            await this._carStoreDbContext.ShoppingCarts.AddAsync(shoppingCart);
            await this._carStoreDbContext.SaveChangesAsync();
        }

        public async Task AddItemToShoppingCart(int shoppingCartId, int customerId, ShoppingCartItem item)
        {
            var shoppingCart =
                this._carStoreDbContext.ShoppingCarts.FirstOrDefault(sc =>
                    sc.Id == shoppingCartId && sc.CustomerId == customerId);

            if (shoppingCart == null)
            {
                throw new InvalidOperationException("The shopping cart does not exist.");
            }

            shoppingCart.Status = Status.Incomplete;
            shoppingCart.ShoppingCartItems.Add(item);

            this._carStoreDbContext.ShoppingCarts.Update(shoppingCart);
            await this._carStoreDbContext.SaveChangesAsync();
        }

        public async Task RemoveItemToShoppingCart(int shoppingCartId, int customerId, ShoppingCartItem item)
        {
            var shoppingCart =
                this._carStoreDbContext.ShoppingCarts.FirstOrDefault(sc =>
                    sc.Id == shoppingCartId && sc.CustomerId == customerId);

            if (shoppingCart == null)
            {
                throw new InvalidOperationException("The shopping cart does not exist.");
            }

            shoppingCart.Status = Status.Incomplete;
            shoppingCart.ShoppingCartItems.Remove(item);

            this._carStoreDbContext.ShoppingCarts.Update(shoppingCart);
            await this._carStoreDbContext.SaveChangesAsync();
        }
    }
}
