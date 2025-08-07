using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VitrineExpress.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<VitrineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VitrineContext") ?? throw new InvalidOperationException("Connection string 'VitrineContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity services
builder.Services.AddRazorPages(options =>
{
    // Exige autenticação para todas as páginas por padrão
    options.Conventions.AuthorizeFolder("/");

    // Permite acesso anônimo à página de Login e Register
    options.Conventions.AllowAnonymousToPage("/Account/Login");
    options.Conventions.AllowAnonymousToPage("/Account/Register");
});


// Configure authentication and authorization
builder.Services.AddAuthentication("VitrineCookie")
    .AddCookie("VitrineCookie", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<VitrineContext>();
    // context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
