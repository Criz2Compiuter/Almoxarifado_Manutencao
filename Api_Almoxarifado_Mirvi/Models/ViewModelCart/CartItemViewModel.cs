namespace Api_Almoxarifado_Mirvi.Models.ViewModelCart
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public ProdutoCartViewModel? Produto { get; set; }
        public int Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public int CartHeaderId { get; set; }
    }
}
