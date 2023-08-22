namespace Api_Almoxarifado_Mirvi.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public byte[] FotoPDF { get; set; }
        public int Quantidade { get; set; }
        public string CodigoDeCompra { get; set; }
    }
}
