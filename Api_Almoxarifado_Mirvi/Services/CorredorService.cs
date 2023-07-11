using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class CorredorService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public CorredorService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Corredor>> FindAllAsync()
        {
            return await _context.Corredor.ToListAsync();
        }

        public async Task InsertAsync(Corredor obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Corredor> FindByIdAsync(int id)
        {
            return await _context.Corredor
                .Include(obj => obj.Almoxarifado)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Corredor.FindAsync(id);
                _context.Corredor.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegreityException("Esse corredor nao pode ser deletado pois a objetos nele");
            }
        }

        public async Task UpdateAsync(Corredor obj)
        {
            bool hasAny = await _context.Corredor.AnyAsync(x => x.Id == obj.Id);
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
        public async Task<List<Corredor>> FindAllInAlmoxarifadoAsync(int almoxarifadoId)
        {
            return await _context.Corredor
                .Where(obj => obj.AlmoxarifadoId == almoxarifadoId)
                .Include(obj => obj.Almoxarifado)
                .ToListAsync();
        }
    }
}
