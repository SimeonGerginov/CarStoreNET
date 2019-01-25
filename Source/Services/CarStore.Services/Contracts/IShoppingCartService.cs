using System.Security.Claims;
using System.Threading.Tasks;

using CarStore.Data.Models;

namespace CarStore.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetShoppingCart(ClaimsPrincipal currentUser);

        Task AddItemToShoppingCart(ClaimsPrincipal currentUser, ShoppingCartItem item);

        Task UpdateQuantityOfShoppingCartItem(ClaimsPrincipal currentUser, int carId, decimal quantity);

        Task RemoveItemFromShoppingCart(ClaimsPrincipal currentUser, int carId);

        Task RemoveShoppingCart(ClaimsPrincipal currentUser);
    }
}
