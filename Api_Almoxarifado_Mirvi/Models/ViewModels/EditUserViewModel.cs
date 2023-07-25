using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
    }
}
