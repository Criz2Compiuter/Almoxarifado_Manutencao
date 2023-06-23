﻿using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class RepartiçõesController : Controller
    {

        private readonly RepartiçõesService _repartiçãoService;
        private readonly CorredorService _corredorService;
        private readonly AlmoxarifadoService _almoxarifadoService;

        public RepartiçõesController(RepartiçõesService repartiçõesService, CorredorService corredorService, AlmoxarifadoService almoxarifadoService)
        {
            _repartiçãoService = repartiçõesService;
            _corredorService = corredorService;
            _almoxarifadoService = almoxarifadoService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _repartiçãoService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var almoxarifado = await _almoxarifadoService.FindAllAsync();
            var viewModel = new FormularioCadastroRepartição { Almoxarifados = almoxarifado };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Repartição repartição)
        {
            if (!ModelState.IsValid)
            {
                await _repartiçãoService.InsertAsync(repartição);
                return RedirectToAction(nameof(Index));
            }
            var almoxarifado = await _almoxarifadoService.FindAllAsync();
            var viewModel = new FormularioCadastroMaquina { Almoxarifados = almoxarifado };
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _repartiçãoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repartiçãoService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegreityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _repartiçãoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _repartiçãoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" }); ;
            }

            List<Almoxarifado> almoxarifado = await _almoxarifadoService.FindAllAsync();
            FormularioCadastroRepartição viewModel = new FormularioCadastroRepartição { Repartição = obj, Almoxarifados = almoxarifado };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Maquina maquina)
        {
            if (!ModelState.IsValid)
            {
                var almoxarifados = await _almoxarifadoService.FindAllAsync();
                var viewModel = new FormularioCadastroMaquina { Almoxarifados = almoxarifados, Maquina = maquina };
            }
            if (id != maquina.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Ids fornecido nao correspondem" });
            }
            try
            {
                await _repartiçãoService.UpdateAsync(maquina);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
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
