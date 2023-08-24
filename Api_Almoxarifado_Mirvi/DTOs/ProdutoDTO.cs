namespace Api_Almoxarifado_Mirvi.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Valor { get; set; }
        public byte[] FotoPDF { get; set; }
        public int Quantidade { get; set; }
        public int? Minimo { get; set; }
        public int? Maximo { get; set; }
    }
}
