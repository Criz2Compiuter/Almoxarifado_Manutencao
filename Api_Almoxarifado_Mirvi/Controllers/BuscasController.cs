using Api_Almoxarifado_Mirvi.Models.Enums;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class BuscasController : Controller
    {

        private readonly BuscasService _buscasService;
        private readonly ProdutosService _produtosService;

        public BuscasController(BuscasService buscasService, ProdutosService produtosService)
        {
            _buscasService = buscasService;
            _produtosService = produtosService;
        }
        public async Task<IActionResult>Index()
        {
            var produtos = await _produtosService.FindAllAsync();
            return View(produtos);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _produtosService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
