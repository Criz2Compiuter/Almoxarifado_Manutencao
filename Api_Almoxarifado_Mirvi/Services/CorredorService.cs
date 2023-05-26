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

        public List<Corredor> FindAll()
        {
            return _context.Corredor.ToList();
        }

        public void Insert(Corredor obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Corredor FindById(int id)
        {
            return _context.Corredor.Include(obj => obj.Almoxarifado).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Corredor.Find(id);
            _context.Corredor.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Corredor obj)
        {
            if (!_context.Corredor.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id nao encontrado");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
