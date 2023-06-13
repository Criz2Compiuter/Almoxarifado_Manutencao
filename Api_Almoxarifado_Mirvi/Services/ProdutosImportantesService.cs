using Api_Almoxarifado_Mirvi.Models;
using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Api_Almoxarifado_Mirvi.Models.Enums;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class ProdutosImportantesService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public ProdutosImportantesService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoImportante>> FindAllAsync()
        {
            return await _context.ProdutoImportante.Include(obj => obj.Prateleiras).Include(obj => obj.Enderecos).ToListAsync();
        }
        public async Task InsertAsync(ProdutoImportante obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<ProdutoImportante> FindByIdAsync(int id)
        {
            return await _context.ProdutoImportante.Include(obj => obj.Prateleiras).Include(obj => obj.Enderecos).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.ProdutoImportante.Find(id);
                _context.ProdutoImportante.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegreityException("Produto nao pode ser deletado");
            }
        }

        public async Task UpdateAsync(ProdutoImportante obj)
        {
            bool hasAny = _context.ProdutoImportante.Any(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id nao encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            } 
            catch (DbUpdateException e) 
            {
                throw new IntegreityException(e.Message);
            }
        }

        public async Task AtualizarProduto(ProdutoImportante obj)
        {
            bool hasAny = _context.ProdutoImportante.Any(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Produto nao encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {

                throw new IntegreityException(e.Message);
            }
        }

        public async Task<List<ProdutoImportante>> ObterProdutosImportantesIndisponíveisAsync()
        {
            return await _context.ProdutoImportante
                .Where(p => p.ProdutoStatusImportante == ProdutoStatusImportante.Indisponivelv || p.ProdutoStatusImportante == ProdutoStatusImportante.LimiteBaixov)
                .Include(p => p.Prateleiras)
                .Include(p => p.Enderecos)
                .ToListAsync();
        }
    }
}
