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

        public List<Prateleira> FindAll()
        {
            return _context.Prateleira.ToList();
        }

        public void Insert(Prateleira obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Prateleira FindById(int id)
        {
            return _context.Prateleira.Include(obj => obj.Corredor).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Prateleira.Find(id);
            _context.Prateleira.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Prateleira obj)
        {
            if (!_context.Prateleira.Any(x => x.Id == obj.Id))
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
