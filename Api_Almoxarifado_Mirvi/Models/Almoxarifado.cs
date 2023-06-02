using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class Almoxarifado
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Insira o {0} do almoxarifado")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} do almoxarifado deve ter entre {2} e {1}")]
        public string Nome { get; set; }
        public ICollection<Corredor> Corredor { get; set; } = new List<Corredor>();

        public Almoxarifado()
        {

        }

        public Almoxarifado(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
