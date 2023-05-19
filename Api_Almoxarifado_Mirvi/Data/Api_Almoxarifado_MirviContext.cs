using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Models;

namespace Api_Almoxarifado_Mirvi.Data
{
    public class Api_Almoxarifado_MirviContext : DbContext
    {
        public Api_Almoxarifado_MirviContext (DbContextOptions<Api_Almoxarifado_MirviContext> options)
            : base(options)
        {
        }

        public DbSet<Almoxarifado> Almoxarifado { get; set; } = default!;
        public DbSet<Corredores> Corredores { get; set; }
        public DbSet<Prateleiras> Prateleiras { get; set; }
        public DbSet<Enderecos> Enderecos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
