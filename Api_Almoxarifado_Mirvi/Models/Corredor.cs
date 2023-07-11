using System.ComponentModel.DataAnnotations;
namespace Api_Almoxarifado_Mirvi.Models
{
    public class Corredor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Insira a {0} do corredor para cadastro")]
        [StringLength(20, MinimumLength = 1,ErrorMessage = "O nome do correodr deve ter no maximo 20 caracter")]
        public string Descricao { get; set; }
        public Almoxarifado Almoxarifado { get; set; }
        public int AlmoxarifadoId { get; set; }
        public ICollection<Prateleira> Prateleiras { get; set; } = new List<Prateleira>();
        public ICollection<Produto> Produto { get; set; } = new List<Produto>();

        public Corredor()
        {
        }

        public Corredor(int id, string descricao, Almoxarifado almoxarifado)
        {
            Id = id;
            Descricao = descricao;
            Almoxarifado = almoxarifado;
        }

        public void AddCorredor(Prateleira cr)
        {
            Prateleiras.Add(cr);
        }

        public void RemoveCorredor(Prateleira cr)
        {
            Prateleiras.Remove(cr);
        }
    }
}
