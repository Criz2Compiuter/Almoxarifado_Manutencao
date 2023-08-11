using Api_Almoxarifado_Mirvi.DTOs;

namespace Api_Almoxarifado_Mirvi.Services {
    public interface ICartService {
        Task<CartDTO> GetCartbyUserIdAsync(string userid);
        Task<CartDTO> UpdateCartAsync(CartDTO cart);
        Task<bool> CleancartAsync(string userId);
        Task<bool> DeleteItemCartAsync(int cartItemId);
    }
}
