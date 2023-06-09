using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class BuscasService
    {

        private readonly Api_Almoxarifado_MirviContext _context;

        public BuscasService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Produto select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }
            return await result
                .Include(x => x.Enderecos)
                .Include(x => x.Enderecos.Prateleiras)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Prateleira, Produto>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Produto select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }
            return await result
                .Include(x => x.Enderecos)
                .Include(x => x.Enderecos.Prateleiras)
                .OrderByDescending(x => x.Data)
                .GroupBy(x => x.Enderecos.Prateleiras)
                .ToListAsync();
        }

        public async Task<List<Produto>> FindByStatusAsync(ProdutoStatus? produtoStatus)
        {
            var result = from obj in _context.Produto select obj;
            if(produtoStatus.HasValue)
            {
                result = result.Where(x => x.Status == produtoStatus);
            }
            return await result
                .Include(x => x.CodigoDeCompra)
                .Include(x => x.Status)
                .OrderByDescending(x => x.Status)
                .ToListAsync();
        }
    }
}

