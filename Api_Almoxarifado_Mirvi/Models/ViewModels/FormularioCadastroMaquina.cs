namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class FormularioCadastroMaquina
    {
        public Maquina Maquina { get; set; }
        public ICollection<Almoxarifado> Almoxarifados { get; set; }
        public int AlmoxarifadoId { get; set; }
    }
}
