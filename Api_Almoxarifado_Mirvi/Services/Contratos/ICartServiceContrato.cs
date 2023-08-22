using Api_Almoxarifado_Mirvi.Models.ViewModelCart;

namespace Api_Almoxarifado_Mirvi.Services.Contratos;

public interface ICartServiceContrato
{
    Task<CartViewModel> GetCartByUsersIdAsync(string userId);
    Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM);
    Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM);
    Task<bool> RemoveItemFromCartAsync(int cartId);
}
