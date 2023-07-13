using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services;
using System.Globalization;
using Api_Almoxarifado_Mirvi.Data;
using Microsoft.AspNetCore.Identity;
using Api_Almoxarifado_Mirvi.Authorization;

namespace Api_Almoxarifado_Mirvi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");

            builder.Services.AddDbContext<Api_Almoxarifado_MirviContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddDbContext<UsuarioDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<UsuarioDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Cargos", policy => policy.AddRequirements(new Cargos("Visitante", "Mecanico", "Administrador")));
            });

            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<TokenService>();

            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<CorredorService>();
            builder.Services.AddScoped<PrateleiraService>();
            builder.Services.AddScoped<ProdutosService>();
            builder.Services.AddScoped<BuscasService>();
            builder.Services.AddScoped<AlmoxarifadoService>();
            builder.Services.AddScoped<RepartiçõesService>();
            builder.Services.AddScoped<MaquinasService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                // Resolve the SeedingService from the service provider
                var seedingService = serviceProvider.GetRequiredService<SeedingService>();

                // Call the Seed method on the SeedingService
                seedingService.Seed(serviceProvider);
            }

            var ptBR = new CultureInfo("pt-BR");
            var localizationOption = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ptBR),
                SupportedCultures = new List<CultureInfo> { ptBR },
                SupportedUICultures = new List<CultureInfo> { ptBR }
            };

            app.UseRequestLocalization(localizationOption);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "customRoute",
                    pattern: "{controller=Home}/{action=IndexM}/{id}/{parameter}"); // Rota personalizada com parâmetros adicionais

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=IndexM}/{id?}"); // Rota padrão
            });

            app.Run();
        }
    }
}