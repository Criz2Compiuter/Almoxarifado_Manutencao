using Api_Almoxarifado_Mirvi.Models;
using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Api_Almoxarifado_Mirvi.Models.Enums;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class ProdutosService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public ProdutosService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> FindAllAsync()
        {
            return await _context.Produto.Include(obj => obj.Prateleiras)
                .Include(obj => obj.Enderecos)
                .Include(obj => obj.Almoxarifado)
                .ToListAsync();
        }

        public async Task InsertAsync(Produto obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> FindByIdAsync(int id)
        {
            return await _context.Produto.Include(obj => obj.Prateleiras)
                .Include(obj => obj.Enderecos)
                .Include(obj => obj.Maquina)
                .Include(obj => obj.Repartição)
                .Include(obj => obj.Almoxarifado)
                .FirstOrDefaultAsync(obj => obj.Id == id);
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
                throw new IntegreityException("Produto nao pode ser deletado");
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
            catch (DbUpdateException e)
            {
                throw new IntegreityException(e.Message);
            }
        }

        public async Task AtualizarProduto(Produto obj)
        {
            bool hasAny = _context.Produto.Any(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Produto nao encontrado");
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

        public async Task<List<Produto>> ObterProdutosIndisponíveisAsync(int minimo, int maximo)
        {
            return await _context.Produto
                .Where(p => p.Status == ProdutoStatus.Indisponivel || p.Status == ProdutoStatus.LimiteBaixo)
                .Include(p => p.Prateleiras)
                .Include(p => p.Enderecos)
                .ToListAsync();
        }

        public async Task<List<Produto>> FindByDescriptionAsync(string searchValue)
        {
            return await _context.Produto
                .Where(p => p.Descricao.Contains(searchValue))
                .ToListAsync();
        }

        public async Task<List<Produto>> FindByAlmoxarifadoAsync(int almoxarifadoId)
        {
            return await _context.Produto
                .Where(p => p.AlmoxarifadoId == almoxarifadoId)
                .Include(obj => obj.Prateleiras)
                .Include(obj => obj.Enderecos)
                .ToListAsync();
        }
        public async Task<List<Produto>> SearchByAlmoxarifadoAsync(int almoxarifadoId, string searchValue)
        {
            var query = _context.Produto.AsQueryable();

            // Filtrar por almoxarifadoId, se for fornecido
            if (almoxarifadoId > 0)
            {
                query = query.Where(p => p.AlmoxarifadoId == almoxarifadoId);
            }

            // Filtrar por descrição, se for fornecida
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Descricao.Contains(searchValue));
            }
            return await query
                .Include(p => p.Prateleiras)
                .Include(p => p.Enderecos)
                .ToListAsync();
        }
        public async Task<List<Produto>> GetProdutosByAlmoxarifadoAsync(string almoxarifadoId)
        {
            var produtos = await _context.Produto
                .Where(p => p.Almoxarifado.Nome == almoxarifadoId)
                .ToListAsync();

            return produtos;
        }
    }
}