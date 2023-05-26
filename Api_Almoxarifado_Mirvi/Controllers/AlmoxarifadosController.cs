using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class AlmoxarifadosController : Controller
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public AlmoxarifadosController(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        // GET: Almoxarifados
        public async Task<IActionResult> Index()
        {
            return _context.Almoxarifado != null ?
                        View(await _context.Almoxarifado.ToListAsync()) :
                        Problem("Entity set 'Api_Almoxarifado_MirviContext.Almoxarifado'  is null.");
        }

        // GET: Almoxarifados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Almoxarifado == null)
            {
                return NotFound();
            }

            var almoxarifado = await _context.Almoxarifado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (almoxarifado == null)
            {
                return NotFound();
            }

            return View(almoxarifado);
        }

        // GET: Almoxarifados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Almoxarifados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Almoxarifado almoxarifado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(almoxarifado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(almoxarifado);
        }

        // GET: Almoxarifados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Almoxarifado == null)
            {
                return NotFound();
            }

            var almoxarifado = await _context.Almoxarifado.FindAsync(id);
            if (almoxarifado == null)
            {
                return NotFound();
            }
            return View(almoxarifado);
        }

        // POST: Almoxarifados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Almoxarifado almoxarifado)
        {
            if (id != almoxarifado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(almoxarifado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlmoxarifadoExists(almoxarifado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(almoxarifado);
        }

        // GET: Almoxarifados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Almoxarifado == null)
            {
                return NotFound();
            }

            var almoxarifado = await _context.Almoxarifado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (almoxarifado == null)
            {
                return NotFound();
            }

            return View(almoxarifado);
        }

        // POST: Almoxarifados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Almoxarifado == null)
            {
                return Problem("Entity set 'Api_Almoxarifado_MirviContext.Almoxarifado'  is null.");
            }
            var almoxarifado = await _context.Almoxarifado.FindAsync(id);
            if (almoxarifado != null)
            {
                _context.Almoxarifado.Remove(almoxarifado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlmoxarifadoExists(int id)
        {
            return (_context.Almoxarifado?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
