using Microsoft.AspNetCore.Mvc;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class BuscasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SimpleSearch()
        {
            return View();
        }
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
