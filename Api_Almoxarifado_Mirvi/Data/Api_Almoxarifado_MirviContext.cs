using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class Api_Almoxarifado_MirviContext : IdentityDbContext
    {

        public Api_Almoxarifado_MirviContext (DbContextOptions<Api_Almoxarifado_MirviContext> options)
            : base(options)
        {
        }

        public DbSet<Almoxarifado> Almoxarifado { get; set; } = default!;
        public DbSet<Corredor> Corredor { get; set; }
        public DbSet<Prateleira> Prateleira { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Maquina> Maquina { get; set; }
        public DbSet<Repartição> Repartição { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    UsuarioId = 1,
                    Nome = "Jose"
                });
        }
    }
}
