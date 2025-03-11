using System.Collections.Immutable;
using Indies.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

// 🔥 Adicione este middleware **antes** de `app.UseRouting();`
app.Use(async (context, next) =>
{
    if (context.Session.GetInt32("UsuarioId") == null && context.Request.Cookies.ContainsKey("UsuarioId"))
    {
        // Se a sessão estiver vazia mas houver um cookie, restaurar os dados
        context.Session.SetInt32("UsuarioId", int.Parse(context.Request.Cookies["UsuarioId"]));
        context.Session.SetString("NomeUsuario", context.Request.Cookies["NomeUsuario"]);
    }

    await next();
});

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();