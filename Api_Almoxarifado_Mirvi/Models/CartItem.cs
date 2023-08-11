namespace Api_Almoxarifado_Mirvi.Models {
    public class CartItem {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProdutoId { get; set; }
        public int CartHeaderId { get; set; }
        public Produto Produto { get; set; } = new Produto();
        public CartHeader CartHeader { get; set; } = new CartHeader();
    }
}
