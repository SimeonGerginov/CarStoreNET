using System.Security.Claims;
using System.Threading.Tasks;

using CarStore.Data.Models;

namespace CarStore.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task CreateShoppingCart(ClaimsPrincipal currentUser);

        Task AddItemToShoppingCart(int shoppingCartId, int customerId, ShoppingCartItem item);

        Task RemoveItemToShoppingCart(int shoppingCartId, int customerId, ShoppingCartItem item);
    }
}
