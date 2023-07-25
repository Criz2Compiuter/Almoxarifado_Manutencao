namespace Api_Almoxarifado_Mirvi.Models
{
    public class HistoricoDesconto
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataDesconto { get; set; }
        public int ProdutoId { get; set; }
        public int QuantidadeDescontada { get; set; }
        public Produto Produto { get; set; }
    }
}
