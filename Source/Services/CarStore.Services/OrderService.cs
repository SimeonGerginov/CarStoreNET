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
    public class OrderService : IOrderService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly CarStoreDbContext _carStoreDbContext;

        public OrderService(UserManager<Customer> userManager, CarStoreDbContext carStoreDbContext)
        {
            this._userManager = userManager;
            this._carStoreDbContext = carStoreDbContext;
        }

        public async Task CreateOrder(ClaimsPrincipal currentUser, int shoppingCartId, decimal totalPrice)
        {
            var customer = await this._userManager.GetUserAsync(currentUser);
            var shoppingCart =
                this._carStoreDbContext.ShoppingCarts.FirstOrDefault(sc =>
                    sc.Id == shoppingCartId && sc.CustomerId == customer.Id);

            if (shoppingCart == null)
            {
                throw new InvalidOperationException("The shopping cart does not exist.");
            }

            var order = new Order
            {
                ShoppingCartId = shoppingCart.Id,
                CustomerId = customer.Id,
                TotalPrice = totalPrice,
                DateAdded = DateTime.UtcNow
            };

            shoppingCart.Status = Status.Complete;

            this._carStoreDbContext.ShoppingCarts.Update(shoppingCart);
            await this._carStoreDbContext.Orders.AddAsync(order);
            await this._carStoreDbContext.SaveChangesAsync();
        }
    }
}
