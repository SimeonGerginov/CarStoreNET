using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using CarStore.Data;
using CarStore.Data.Models;
using CarStore.Data.Models.Enums;
using CarStore.Services.Contracts;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ShoppingCart> GetShoppingCart(ClaimsPrincipal currentUser)
        {
            var customer = await this._userManager.GetUserAsync(currentUser);
            
            return this._carStoreDbContext.ShoppingCarts
                .Include(sc => sc.Customer)
                .Include(sc => sc.ShoppingCartItems)
                .ThenInclude(sci => sci.Car)
                .FirstOrDefault(sc => sc.CustomerId == customer.Id && sc.Status != Status.Complete
                                                                    && sc.Status != Status.Deleted);
        }

        public async Task AddItemToShoppingCart(ClaimsPrincipal currentUser, ShoppingCartItem item)
        {
            var customer = await this._userManager.GetUserAsync(currentUser);
            var shoppingCart =
                this._carStoreDbContext.ShoppingCarts
                    .Include(sc => sc.Customer)
                    .FirstOrDefault(sc => sc.CustomerId == customer.Id && sc.Status != Status.Complete);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart()
                {
                    CustomerId = customer.Id,
                    Status = Status.Created
                };

                await this._carStoreDbContext.ShoppingCarts.AddAsync(shoppingCart);
                await this._carStoreDbContext.SaveChangesAsync();
            }

            if (shoppingCart.ShoppingCartItems.Any(i => i.CarId == item.CarId))
            {
                return;
            }

            shoppingCart.Status = Status.Incomplete;
            shoppingCart.ShoppingCartItems.Add(item);

            this._carStoreDbContext.ShoppingCarts.Update(shoppingCart);
            await this._carStoreDbContext.SaveChangesAsync();
        }

        public async Task UpdateQuantityOfShoppingCartItem(ClaimsPrincipal currentUser, int carId, decimal quantity)
        {
            var customer = await this._userManager.GetUserAsync(currentUser);
            var shoppingCart =
                this._carStoreDbContext.ShoppingCarts
                    .Include(sc => sc.Customer)
                    .Include(sc => sc.ShoppingCartItems)
                    .FirstOrDefault(sc => sc.CustomerId == customer.Id && sc.Status != Status.Complete 
                                                                        && sc.Status != Status.Deleted);

            if (shoppingCart == null)
            {
                throw new InvalidOperationException("The shopping cart does not exist.");
            }

            ShoppingCartItem itemToRemove = null;
            foreach (var item in shoppingCart.ShoppingCartItems)
            {
                if (item.CarId == carId)
                {
                    if (quantity <= 0)
                    {
                        itemToRemove = item;
                    }
                    else
                    {
                        item.Quantity = quantity;
                    }

                    break;
                }
            }

            if (itemToRemove != null)
            {
                shoppingCart.ShoppingCartItems.Remove(itemToRemove);
            }

            this._carStoreDbContext.ShoppingCarts.Update(shoppingCart);
            await this._carStoreDbContext.SaveChangesAsync();
        }

        public async Task RemoveItemFromShoppingCart(ClaimsPrincipal currentUser, int carId)
        {
            var customer = await this._userManager.GetUserAsync(currentUser);
            var shoppingCart =
                this._carStoreDbContext.ShoppingCarts
                    .Include(sc => sc.Customer)
                    .FirstOrDefault(sc => sc.Customer.Id == customer.Id && sc.Status != Status.Complete);

            if (shoppingCart == null)
            {
                throw new InvalidOperationException("The shopping cart does not exist.");
            }

            ShoppingCartItem itemToRemove = null;
            foreach (var item in shoppingCart.ShoppingCartItems)
            {
                if (item.CarId == carId)
                {
                    itemToRemove = item;
                    break;
                }
            }

            if (itemToRemove != null)
            {
                shoppingCart.ShoppingCartItems.Remove(itemToRemove);
            }

            this._carStoreDbContext.ShoppingCarts.Update(shoppingCart);
            await this._carStoreDbContext.SaveChangesAsync();
        }

        public async Task RemoveShoppingCart(ClaimsPrincipal currentUser)
        {
            var customer = await this._userManager.GetUserAsync(currentUser);
            var shoppingCart =
                this._carStoreDbContext.ShoppingCarts
                    .Include(sc => sc.Customer)
                    .FirstOrDefault(sc => sc.Customer.Id == customer.Id && sc.Status != Status.Complete);

            if (shoppingCart == null)
            {
                throw new InvalidOperationException("The shopping cart does not exist.");
            }

            shoppingCart.Status = Status.Deleted;

            this._carStoreDbContext.ShoppingCarts.Update(shoppingCart);
            await this._carStoreDbContext.SaveChangesAsync();
        }
    }
}
