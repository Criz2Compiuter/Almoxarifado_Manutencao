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
        public ICollection<Prateleira> Prateleira { get; set; } =  new List<Prateleira>();
        public ICollection<Endereco> Enderecos { get; set; } =  new List<Endereco>();
        public ICollection<Produto> Produto { get; set; } =  new List<Produto>();

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
