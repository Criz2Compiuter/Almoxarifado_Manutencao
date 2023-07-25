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
            return await _context.Produto.ToListAsync();
        }

        public async Task InsertAsync(Produto obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> FindByIdAsync(int id)
        {
            return await _context.Produto
                .Include(obj => obj.Corredor)
                .Include(obj => obj.Prateleiras)
                .Include(obj => obj.Maquina)
                .Include(obj => obj.Repartição)
                .Include(obj => obj.Almoxarifado)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Produto.FindAsync(id);
            if (obj == null)
            {
                throw new NotFoundException("Produto não encontrado");
            }
            _context.Produto.Remove(obj);
            await _context.SaveChangesAsync();
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
            catch (DbUpdateConcurrencyException e)
            {
                throw new IntegreityException(e.Message);
            }
        }

        public async Task<List<Produto>> ObterProdutosIndisponíveisAsync(int minimo, int maximo, int almoxarifadoId)
        {
            return await _context.Produto
                .Where(p => p.AlmoxarifadoId ==  almoxarifadoId)
                .Where(p => p.Status == ProdutoStatus.Indisponivel || p.Status == ProdutoStatus.LimiteBaixo)
                .Include(p => p.Prateleiras)
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
                .ToListAsync();
        }
        public async Task<List<Produto>> SearchByAlmoxarifadoAsync(int almoxarifadoId, string searchValue)
        {
            var query = _context.Produto.AsQueryable();

            if (almoxarifadoId > 0)
            {
                query = query.Where(p => p.AlmoxarifadoId == almoxarifadoId);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Descricao.Contains(searchValue));
            }
            return await query
                .Include(p => p.Prateleiras)
                .ToListAsync();
        }
        public async Task<List<Produto>> GetProdutosByAlmoxarifadoAsync(int almoxarifadoId)
        {
            var produtos = await _context.Produto
                .Where(p => p.Almoxarifado.Id == almoxarifadoId)
                .ToListAsync();
            return produtos;
        }

        public async Task<List<Produto>> FindAllInAlmoxarifadoAsync(int almoxarifadoId)
        {
            return await _context.Produto
                .Where(obj => obj.AlmoxarifadoId == almoxarifadoId)
                .Include(obj => obj.Corredor)
                .Include(obj => obj.Prateleiras)
                .Include(obj => obj.Almoxarifado)
                .Include(obj => obj.Maquina)
                .Include(obj => obj.Repartição)
                .ToListAsync();
        }
        public async Task<List<Produto>> FindAllInReparticaoAsync(int? reparticaoId)
        {
            return await _context.Produto
                .Where(obj => obj.RepartiçãoId == reparticaoId && obj.AlmoxarifadoId == 1)
                .Include(obj => obj.Prateleiras)
                .Include(obj => obj.Almoxarifado)
                .Include(obj => obj.Repartição)
                .ToListAsync();
        }
        public async Task<List<Produto>> FindAllInMaquinaAsync(int? maquinaId)
        {
            return await _context.Produto
                .Where(obj => obj.MaquinaId == maquinaId && obj.AlmoxarifadoId == 2)
                .Include(obj => obj.Prateleiras)
                .Include(obj => obj.Almoxarifado)
                .Include(obj => obj.Maquina)
                .ToListAsync();
        }

        public async Task DeduzirQuantidadeAsync(int productId, int quantidade)
        {
            var produto = await FindByIdAsync(productId);
            if (produto == null)
            {
                throw new NotFoundException("Produto não encontrado");
            }

            if (produto.Quantidade < quantidade)
            {
                throw new Exception("Quantidade insuficiente do produto");
            }

            produto.Quantidade -= quantidade;

            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegreityException(e.Message);
            }
        }

        public async Task<List<HistoricoDesconto>> ObterHistoricoDescontosPorUsuarioAsync(string nomeUsuario)
        {
            var historicosDescontos = await _context.HistoricosDescontos
                .Where(h => h.NomeUsuario == nomeUsuario)
                .OrderByDescending(h => h.DataDesconto)
                .ToListAsync();

            return historicosDescontos;
        }

        public async Task<List<HistoricoDesconto>> GetHistoricoByNomeUsuario(string nomeUsuario)
        {
            var historicos = await _context.HistoricosDescontos
                .Include(h => h.Produto)
                .Where(h => h.NomeUsuario == nomeUsuario)
                .ToListAsync();

            return historicos;
        }

        public async Task DescontarQuantidadeAsync(int productId, int quantidade, string nomeUsuario)
        {
            var produto = await FindByIdAsync(productId);
            if (produto == null)
            {
                throw new NotFoundException("Produto não encontrado");
            }

            if (produto.Quantidade < quantidade)
            {
                throw new Exception("Quantidade insuficiente do produto");
            }

            produto.Quantidade -= quantidade;

            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegreityException(e.Message);
            }

            var historicoDesconto = new HistoricoDesconto
            {
                NomeUsuario = nomeUsuario,
                ProdutoId = productId,
                QuantidadeDescontada = quantidade,
                DataDesconto = DateTime.Now
            };

            _context.HistoricosDescontos.Add(historicoDesconto);
            await _context.SaveChangesAsync();
        }

        public async Task RegistrarDesconto(string nomeUsuario, Produto produto, int quantidadeDescontada)
        {
            var historicoDesconto = new HistoricoDesconto
            {
                NomeUsuario = nomeUsuario,
                QuantidadeDescontada = quantidadeDescontada,
                DataDesconto = DateTime.Now,
                Produto = produto
            };

            _context.HistoricosDescontos.Add(historicoDesconto);
            await _context.SaveChangesAsync();
        }
    }
}