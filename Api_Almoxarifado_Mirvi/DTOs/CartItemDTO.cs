using Api_Almoxarifado_Mirvi.Models;

namespace Api_Almoxarifado_Mirvi.DTOs {
    public class CartItemDTO {
        public int Id { get; set; }
        public ProdutoDTO Produto { get; set; } = new ProdutoDTO();
        public int Quantity { get; set; }
        public int ProdutoId { get; set; }
        public int CartHeaderId { get; set; }
        //public CartHeaderDTO CartHeader { get; set; } = new CartHeader();
    }
}
