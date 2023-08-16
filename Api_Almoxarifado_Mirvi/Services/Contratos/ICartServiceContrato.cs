using Api_Almoxarifado_Mirvi.DTOs;
using Api_Almoxarifado_Mirvi.Models.ViewModels;

namespace Api_Almoxarifado_Mirvi.Services.Contratos
{
    public interface ICartServiceContrato
    {
        Task<CartDTO> GetCartByUserIdAsync(string userId);
        Task<CartDTO> UpdateCartAsync(CartDTO cart);
        Task<bool> CleanCartAsync(string userId);
        Task<bool> DeleteItemCartAsync(int cartItemId);

    }
}
