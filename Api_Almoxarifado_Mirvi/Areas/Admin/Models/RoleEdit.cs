using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace Api_Almoxarifado_Mirvi.Areas.Admin.Models
{
    public class RoleEdit
    {
        public IdentityRole? Role{ get; set; }
        public IEnumerable<IdentityUser>? Members { get; set; }
        public IEnumerable<IdentityUser>? NonMembers { get; set; }
    }
}
