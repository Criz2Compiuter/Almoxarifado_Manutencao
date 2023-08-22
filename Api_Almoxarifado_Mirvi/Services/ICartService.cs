using Api_Almoxarifado_Mirvi.DTOs;

namespace Api_Almoxarifado_Mirvi.Services;

public interface ICartService
{
    Task<CartDTO> GetCartByUserIdAsync(string userId);
    Task<CartDTO> UpdateCartAsync(CartDTO cart);
    Task<bool> CleanCartAsync(string userId);
    Task<bool> DeleteCartAsync(int cartItemId);
}
