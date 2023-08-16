using Api_Almoxarifado_Mirvi.Models;

namespace Api_Almoxarifado_Mirvi.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Descriicao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public Almoxarifado Almoxarifado { get; set; }
        public long Quantidade { get; set; }
        public string ImageURL { get; set; } = string.Empty;
    }
}
