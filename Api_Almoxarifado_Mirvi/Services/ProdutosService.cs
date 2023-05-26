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

        public List<Produto> FindAll()
        {
            return _context.Produto.ToList();
        }

        public void Insert(Produto obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Produto FindById(int id)
        {
            return _context.Produto.Include(obj => obj.Prateleiras).Include(obj => obj.Enderecos).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Produto.Find(id);
            _context.Produto.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Produto obj)
        {
            if (!_context.Produto.Any(x => x.Id == obj.Id))
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
