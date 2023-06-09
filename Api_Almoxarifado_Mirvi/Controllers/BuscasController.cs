using Api_Almoxarifado_Mirvi.Models.Enums;
using Api_Almoxarifado_Mirvi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class BuscasController : Controller
    {

        private readonly BuscasService _buscasService;

        public BuscasController(BuscasService buscasService)
        {
            _buscasService = buscasService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _buscasService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _buscasService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
        public async Task<IActionResult> StatusSearch(ProdutoStatus? produtoStatus)
        {
            if (!produtoStatus.HasValue)
            {
                produtoStatus = ProdutoStatus.Indisponivel;
            }
            ViewData["ProdutoStatus"] = produtoStatus.Value.ToString("Indisponivel");
            var result = await _buscasService.FindByStatusAsync(produtoStatus);
            return View(result);
        }
    }
}
