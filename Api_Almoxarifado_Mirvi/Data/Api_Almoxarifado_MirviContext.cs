using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Models;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class Api_Almoxarifado_MirviContext : DbContext
    {

        public Api_Almoxarifado_MirviContext (DbContextOptions<Api_Almoxarifado_MirviContext> options)
            : base(options)
        {
        }

        public DbSet<Almoxarifado> Almoxarifado { get; set; } = default!;
        public DbSet<Corredor> Corredor { get; set; }
        public DbSet<Prateleira> Prateleira { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutoImportante> ProdutoImportante{ get; set; }
    }
}
