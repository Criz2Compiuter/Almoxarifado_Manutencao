using Microsoft.EntityFrameworkCore;
using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Api_Almoxarifado_Mirvi.Services.CartApi;
using Api_Almoxarifado_Mirvi.Services.ViewApi;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");

builder.Services.AddDbContext<Api_Almoxarifado_MirviContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICartApiService, CartApiService>();
builder.Services.AddScoped<ICartViewService, CartViewService>();
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<CorredorService>();
builder.Services.AddScoped<PrateleiraService>();
builder.Services.AddScoped<ProdutosService>();
builder.Services.AddScoped<BuscasService>();
builder.Services.AddScoped<AlmoxarifadoService>();
builder.Services.AddScoped<RepartiçõesService>();
builder.Services.AddScoped<MaquinasService>();

builder.Services.AddHttpClient();

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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireUserAdminMecanicoRole",
        policy => policy.RequireRole("User", "Admin", "Mecanico"));

    options.AddPolicy("RequireAdminMacanico",
        policy => policy.RequireRole("Mecanico", "Admin"));

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdminClaimAccess",
        policy => policy.RequireClaim("CadastradoEm"));
    
    options.AddPolicy("IsAdminClaimAccess",
        policy => policy.RequireClaim("IsAdmin", "true"));

    options.AddPolicy("IsMecanicoClaimAccess",
        policy => policy.RequireClaim("IsMecanico", "true"));

    options.AddPolicy("IsFuncionarioClaimAccess",
        policy => policy.RequireClaim("IsFuncionario", "true"));

});

builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRolesInitial>();
builder.Services.AddScoped<ISeedUserClaimsInitial, SeedUserClaimsInitial>();

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

await CriarPerfisUsuariosAsync(app);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "MinhaArea",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "customRoute",
        pattern: "{controller=Account}/{action=Login}/{id}/{parameter}"); 

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}"); 
});

app.Run();
async Task CriarPerfisUsuariosAsync(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        //var service = scope?.ServiceProvider.GetService<ISeedUserRoleInitial>();
        //await service.SeedRolesAsync();
        //await service.SeedUsersAsync();

        var service = scope.ServiceProvider.GetService<ISeedUserClaimsInitial>();
        await service.SeedUserClaims();
    }
}