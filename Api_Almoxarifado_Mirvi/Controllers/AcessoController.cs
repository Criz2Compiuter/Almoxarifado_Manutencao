using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AcessoController : Controller
    {
        [HttpGet]
        [Authorize(Policy = "Cargos")]
        public IActionResult Get()
        {
            return Ok("Acesso permitido!");
        }
    }
}
