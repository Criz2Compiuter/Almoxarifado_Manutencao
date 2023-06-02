using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class PrateleiraService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public PrateleiraService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Prateleira>> FindAllAsync()
        {
            return await _context.Prateleira.OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task InsertAsync(Prateleira obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Prateleira> FindByIdAsync(int id)
        {
            return await _context.Prateleira.Include(obj => obj.Corredor).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Prateleira.FindAsync(id);
                _context.Prateleira.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegreityException("Nao e possivel remover essa prateleira pois a objetos dentro dela");
            }
        }

        public async Task UpdateAsync(Prateleira obj)
        {
            bool hasAny = await _context.Prateleira.AnyAsync(x => x.Id == obj.Id);
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
    }
}
