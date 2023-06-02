using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class EnderecoService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public EnderecoService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Endereco>> FindAllAsync()
        {
            return await _context.Endereco.ToListAsync();
        }

        public async Task InsertAsync(Endereco obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Endereco> FindByIdAsync(int id)
        {
            return await _context.Endereco.Include(obj => obj.Prateleiras).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.Endereco.Find(id);
                _context.Endereco.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegreityException("Nao e possivel deletar esse endereco pois a objetos dentro dele");
            }
        }

        public async Task UpdateAsync(Endereco obj)
        {
            bool hasAny = _context.Endereco.Any(x => x.Id == obj.Id);
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
