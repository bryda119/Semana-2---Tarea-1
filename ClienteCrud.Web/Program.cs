using ClienteCrud.Business.Services;
using ClienteCrud.Data;
using ClienteCrud.Data.Repositories;
using ClienteCrud.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Falta ConnectionStrings:DefaultConnection en appsettings.json");

// Detecta la versión del servidor sin exigir que la BD exista todavía.
var serverVersion = ServerVersion.AutoDetect(
    "Server=localhost;Port=3306;User=root;Password=;");

// ORM: Entity Framework Core + MySQL (Pomelo). La BD se crea al iniciar la app.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// Inyección de dependencias por capas
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// Crear la base de datos y la tabla Clientes desde el modelo ORM
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clientes}/{action=Index}/{id?}");

app.Run();
