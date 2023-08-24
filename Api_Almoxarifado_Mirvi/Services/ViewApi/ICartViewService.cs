using Api_Almoxarifado_Mirvi.Models;

namespace Api_Almoxarifado_Mirvi.Services.ViewApi
{
    public interface ICartViewService
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task<Cart> AddItemToCartAsync(Cart cart);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task<bool> RemoveItemFromCartAsync(int cartId);
    }
}
