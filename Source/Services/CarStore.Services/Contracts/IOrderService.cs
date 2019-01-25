using System.Security.Claims;
using System.Threading.Tasks;

namespace CarStore.Services.Contracts
{
    public interface IOrderService
    {
        Task CreateOrder(ClaimsPrincipal currentUser, int shoppingCartId, decimal totalPrice);
    }
}
