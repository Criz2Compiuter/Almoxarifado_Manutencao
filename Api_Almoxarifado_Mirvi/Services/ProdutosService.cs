using Api_Almoxarifado_Mirvi.Models;
using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Services.Exceptions;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class ProdutoService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public ProdutoService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> FindAllAsync()
        {
            return await _context.Produto.ToListAsync();
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
                throw new IntegreityException(e.Message);
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
            catch (DbUpdateConcurrencyException e) 
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
