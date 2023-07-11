using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class RepartiçõesService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public RepartiçõesService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Repartição>> FindAllAsync()
        {
            return await _context.Repartição.ToListAsync();
        }

        public async Task InsertAsync(Repartição obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Repartição> FindByIdAsync(int id)
        {
            return await _context.Repartição.Include(obj => obj.Almoxarifado).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Repartição.FindAsync(id);
                _context.Repartição.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegreityException("Nao e possivel remover essa prateleira pois a objetos dentro dela");
            }
        }

        public async Task UpdateAsync(Repartição obj)
        {
            bool hasAny = await _context.Repartição.AnyAsync(x => x.Id == obj.Id);
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

        public async Task<List<Repartição>> FindAllInAlmoxarifadoAsync(int almoxarifadoId)
        {
            return await _context.Repartição
                .Where(obj => obj.AlmoxarifadoId == almoxarifadoId)
                .Include(obj => obj.Almoxarifado)
                .ToListAsync();
        }
    }
}
