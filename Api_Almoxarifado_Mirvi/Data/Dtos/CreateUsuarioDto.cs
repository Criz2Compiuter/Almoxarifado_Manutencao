﻿using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
