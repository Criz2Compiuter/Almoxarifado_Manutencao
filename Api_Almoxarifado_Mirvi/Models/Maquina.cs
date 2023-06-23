using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class Maquina
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Insira o Nome da Prateleira para cadastro")]
        [StringLength(20, ErrorMessage = "O {0} da prateleira deve ter {1} caracter")]
        public string Nome { get; set; }
        public Almoxarifado Almoxarifado { get; set; }
        public int AlmoxarifadoId { get; set; }
        public ICollection<Produto>? Produto { get; set; }

        public Maquina()
        {
        }

        public Maquina(int id, string nome, Almoxarifado almoxarifado, int almoxarifadoId)
        {
            Id = id;
            Nome = nome;
            Almoxarifado = almoxarifado;
            AlmoxarifadoId = almoxarifadoId;
        }
    }
}
