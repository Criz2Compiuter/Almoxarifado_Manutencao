namespace Api_Almoxarifado_Mirvi.Models.ViewModelCart
{
    public class CartViewModel
    {
        public CartHeaderViewModel CartHeader { get; set; } = new CartHeaderViewModel();
        public IEnumerable<CartItemViewModel>? CartItems { get; set; }
    }
}
