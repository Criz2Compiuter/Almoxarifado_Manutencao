namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class FormularioCadastroRepartição
    {
        public Repartição Repartição { get; set; }
        public ICollection<Almoxarifado> Almoxarifados { get; set; }
    }
}
