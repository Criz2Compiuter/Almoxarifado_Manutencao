using Api_Almoxarifado_Mirvi.DTOs;
using Api_Almoxarifado_Mirvi.Models.ViewModels;

namespace Api_Almoxarifado_Mirvi.Services {
    public interface ICartService {
        Task<CartViewModel> GetCartByUserIdAsync(string userI);
        Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM);
        Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM);
        Task<bool> RemoveItemFromCartAsync(int cartId);
    }
}
