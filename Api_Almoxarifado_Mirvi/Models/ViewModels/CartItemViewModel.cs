namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public Produto? Produto { get; set; }
        public int Quantity { get; set; } = 1;
        public int ProdutoId { get; set; }
        public int CartHeaderId { get; set; }
    }
}
