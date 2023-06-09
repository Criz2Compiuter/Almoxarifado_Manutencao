using Api_Almoxarifado_Mirvi.Models;
using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Api_Almoxarifado_Mirvi.Models.Enums;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class ProdutosService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public ProdutosService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> FindAllAsync()
        {
            return await _context.Produto.Include(obj => obj.Prateleiras).Include(obj => obj.Enderecos).ToListAsync();
        }

        public async Task InsertAsync(Produto obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> FindByIdAsync(int id)
        {
            return await _context.Produto.Include(obj => obj.Prateleiras).Include(obj => obj.Enderecos).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.Produto.Find(id);
                _context.Produto.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegreityException("Produto nao pode ser deletado");
            }
        }

        public async Task UpdateAsync(Produto obj)
        {
            bool hasAny = _context.Produto.Any(x => x.Id == obj.Id);
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

        public async Task AtualizarProduto(Produto obj)
        {
            bool hasAny = _context.Produto.Any(x => x.Id == obj.Id);
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
    }
}
