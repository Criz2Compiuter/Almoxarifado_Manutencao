using Api_Almoxarifado_Mirvi.DTOs;

namespace Api_Almoxarifado_Mirvi.Services.CartApi
{
    public interface ICartApiService
    {
        Task<CartDTO> GetCartByUserIdAsync(string userId);
        Task<CartDTO> UpdatecartAsync(CartDTO cart);
        Task<bool> CleanCartAsync(string userId);
        Task<bool> DeleteItemCartAsync(int userId);
    }
}
