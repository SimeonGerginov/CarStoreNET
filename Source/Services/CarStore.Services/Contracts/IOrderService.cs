using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using CarStore.Data.Models;

namespace CarStore.Services.Contracts
{
    public interface IOrderService
    {
        Task CreateOrder(ClaimsPrincipal currentUser, int shoppingCartId);

        Task<IEnumerable<Order>> GetAllOrdersOfCustomer(ClaimsPrincipal currentUser);
    }
}
