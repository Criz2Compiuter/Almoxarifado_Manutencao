using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Api_Almoxarifado_Mirvi.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            builder.Services
                .AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<Api_Almoxarifado_MirviContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            builder.Services.AddSingleton<IAuthorizationHandler, CargosAuthorization>();

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9780ouijhluo89uihjuio")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CargosNecessario", policy =>
                     policy.AddRequirements(new CargosNecessario("Mecanico", "Administrador", "Visitante"))
                );
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<CorredorService>();
            builder.Services.AddScoped<PrateleiraService>();
            builder.Services.AddScoped<ProdutosService>();
            builder.Services.AddScoped<BuscasService>();
            builder.Services.AddScoped<AlmoxarifadoService>();
            builder.Services.AddScoped<RepartiçõesService>();
            builder.Services.AddScoped<MaquinasService>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<TokenService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var seedingService = serviceProvider.GetRequiredService<SeedingService>();

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

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
                    pattern: "{controller=Home}/{action=IndexM}/{id}/{parameter}"); 

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=IndexM}/{id?}");
            });

            app.MapControllers();

            app.Run();
        }
    }
}