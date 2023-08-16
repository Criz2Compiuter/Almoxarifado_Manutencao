using Api_Almoxarifado_Mirvi.Models.ViewModels;

namespace Api_Almoxarifado_Mirvi.Services.Contratos
{
    public interface ICartServiceContrato
    {
        Task<CartViewModel> GetCartByUserIdAsync(string userId);
        Task<CartViewModel> AddItemToCartAsync(CartViewModel cartM);
        Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM);
        Task<bool> RemoveItemFromCartAsync(int cartId);

    }
}
