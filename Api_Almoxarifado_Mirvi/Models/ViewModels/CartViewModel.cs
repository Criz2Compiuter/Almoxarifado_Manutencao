namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class CartViewModel
    {
        public CartHeaderViewModel CartHeader { get; set; } = new CartHeaderViewModel();
        public IEnumerable<CartItemViewModel>? cartItems { get; set; }
    }
}
