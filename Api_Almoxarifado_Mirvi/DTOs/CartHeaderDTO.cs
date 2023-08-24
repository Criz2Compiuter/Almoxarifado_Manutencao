using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.DTOs
{
    public class CartHeaderDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is Required")]
        public string UserId { get; set; } = string.Empty;
    }
}
