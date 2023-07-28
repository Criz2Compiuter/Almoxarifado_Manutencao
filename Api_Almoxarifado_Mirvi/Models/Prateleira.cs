using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class Prateleira
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Insira o Nome da Prateleira para cadastro")]
        [StringLength(20, ErrorMessage = "O {0} da prateleira deve ter {1} caracter")]
        public string Nome { get; set; }
        public Corredor Corredor { get; set; }
        public int CorredorId { get; set; }
        public ICollection<Produto>? Produto { get; set; }

        public Prateleira()
        {
        }

        public Prateleira(int id, string nome, Corredor corredor)
        {
            Id = id;
            Nome = nome;
            Corredor = corredor;
        }
    }
}
