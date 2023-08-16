using Api_Almoxarifado_Mirvi.DTOs;
using Api_Almoxarifado_Mirvi.Models.ViewModels;

namespace Api_Almoxarifado_Mirvi.Services {
    public interface ICartService {
        Task<CartViewModel> GetCartByUserIdAsync(string userId, string token);
        Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM, string token);
        Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM, string token);
        Task<bool> RemoveItemFromCartAsync(int cartId, string token);
    }
}
