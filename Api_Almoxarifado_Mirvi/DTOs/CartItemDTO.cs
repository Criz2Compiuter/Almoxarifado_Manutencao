namespace Api_Almoxarifado_Mirvi.DTOs
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public ProdutoDTO Produto { get; set; } = new ProdutoDTO();
        public int Quantidade { get; set; } = 1;
        public int ProdutoId { get; set; }
        public int CartHeaderId { get; set; }
    }
}
