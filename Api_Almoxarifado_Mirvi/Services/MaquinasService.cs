using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class MaquinasService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public MaquinasService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Maquina>> FindAllAsync()
        {
            return await _context.Maquina.Include(obj => obj.Almoxarifado).OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task InsertAsync(Maquina obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Maquina> FindByIdAsync(int id)
        {
            return await _context.Maquina.Include(obj => obj.Almoxarifado).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Maquina.FindAsync(id);
                _context.Maquina.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegreityException("Nao e possivel remover essa prateleira pois a objetos dentro dela");
            }
        }

        public async Task UpdateAsync(Maquina obj)
        {
            bool hasAny = await _context.Maquina.AnyAsync(x => x.Id == obj.Id);
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
