using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Enums;
using VitrineExpress.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração global de cultura
var defaultCulture = new CultureInfo("pt-BR");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(defaultCulture);
    options.SupportedCultures = new List<CultureInfo> { defaultCulture };
    options.SupportedUICultures = new List<CultureInfo> { defaultCulture };
    CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
    CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
});

// Banco de dados
builder.Services.AddDbContext<VitrineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VitrineContext")
        ?? throw new InvalidOperationException("Connection string 'VitrineContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Autenticação via Cookie
builder.Services.AddAuthentication("VitrineCookie")
    .AddCookie("VitrineCookie", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

// Autorização com policy para Admin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(TipoUsuario.ADMIN.ToString()));

    options.AddPolicy("CLienteOnly", policy =>
       policy.RequireRole(TipoUsuario.CLIENTE.ToString()));

    options.AddPolicy("LojistaOnly", policy =>
       policy.RequireRole(TipoUsuario.LOJISTA.ToString()));

    options.AddPolicy("AdminOrLojista", policy =>
        policy.RequireRole(
            TipoUsuario.ADMIN.ToString(),
            TipoUsuario.LOJISTA.ToString()
        ));
});

// Configuração de páginas e restrições
builder.Services.AddRazorPages(options =>
{
    // Exige autenticação para todas as páginas por padrão
    //options.Conventions.AuthorizeFolder("/");

    // Permite acesso anônimo para login e registro
    options.Conventions.AllowAnonymousToPage("/Account/Login");
    options.Conventions.AllowAnonymousToPage("/Account/Register");

    // Restringe o acesso a páginas específicas
    options.Conventions.AuthorizeFolder("/Usuarios", "AdminOnly");

    options.Conventions.AuthorizeFolder("/Carrinhos/Index", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Enderecos/Index", "AdminOnly");
    options.Conventions.AuthorizeFolder("/ItensCarrinho/Index", "AdminOnly");
    options.Conventions.AuthorizeFolder("/ItensPedido/Index", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Pedidos/Index", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Produtos/Index", "AdminOnly");

});

// Registra o serviço de hashing de senha para ser usado na injeção de dependência.
builder.Services.AddSingleton<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();


var app = builder.Build();

// Cultura global
var localizationOptions = app.Services
    .GetRequiredService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>()
    .Value;
app.UseRequestLocalization(localizationOptions);

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// Executa migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VitrineContext>();

    if (app.Environment.IsDevelopment())
    {
        context.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();