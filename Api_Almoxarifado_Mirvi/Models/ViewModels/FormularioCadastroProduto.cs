namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class FormularioCadastroProduto
    {
        public Produto Produto { get; set; }
        public ICollection<Almoxarifado> Almoxarifado { get; set; }
        public ICollection<Endereco>? Endereco { get; set; }
        public ICollection<Prateleira> Prateleira { get; set; }
        public ICollection<Maquina>? Maquina { get; set; }
        public ICollection<Repartição>? Repartição { get; set; }
        public int IdAlmoxarifado { get; set; }
    }
}
