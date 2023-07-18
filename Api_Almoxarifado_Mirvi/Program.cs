using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api_Almoxarifado_Mirvi.Models;
using Microsoft.Extensions.Options;
using Api_Almoxarifado_Mirvi.Services;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

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

            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<CorredorService>();
            builder.Services.AddScoped<PrateleiraService>();
            builder.Services.AddScoped<ProdutosService>();
            builder.Services.AddScoped<BuscasService>();
            builder.Services.AddScoped<AlmoxarifadoService>();
            builder.Services.AddScoped<RepartiçõesService>();
            builder.Services.AddScoped<MaquinasService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<Api_Almoxarifado_MirviContext>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "AspNetCore.Cookies";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.SlidingExpiration = true;
                });

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

            app.UseAuthentication();
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