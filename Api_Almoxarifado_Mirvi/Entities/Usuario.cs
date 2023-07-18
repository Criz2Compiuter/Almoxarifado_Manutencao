using Api_Almoxarifado_Mirvi.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        [Required, MaxLength(30, ErrorMessage = "Nome nao pode exceder 30 caracteres")]
        public string? Nome { get; set; }
        public Cargos NomeCargos{ get; set; }

    }
}
