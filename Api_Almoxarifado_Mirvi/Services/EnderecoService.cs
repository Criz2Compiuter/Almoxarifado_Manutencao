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

        public List<Endereco> FindAll()
        {
            return _context.Endereco.ToList();
        }

        public void Insert(Endereco obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Endereco FindById(int id)
        {
            return _context.Endereco.Include(obj => obj.Prateleiras).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Endereco.Find(id);
            _context.Endereco.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Endereco obj)
        {
            if (!_context.Endereco.Any(x => x.Id == obj.Id))
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
