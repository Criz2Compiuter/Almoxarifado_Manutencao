namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class FormularioCadastroPrateleira
    {
        public Prateleira Prateleira { get; set; }
        public ICollection<Corredor> Corredores { get; set; }
    }
}
