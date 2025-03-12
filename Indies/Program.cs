using System.Collections.Immutable;
using Indies.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Usuarios/Entrar"; 
        options.LogoutPath = "/Usuarios/Logout";
        options.AccessDeniedPath = "/Home/AcessoNegado"; 
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; 
        options.Cookie.SameSite = SameSiteMode.Lax; 
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

builder.Services.AddAuthorization(); 

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.Use(async (context, next) =>
{
    // Verificando se o cookie "UsuarioId" existe antes de adicionar na sessão
    if (context.Session.GetInt32("UsuarioId") == null && context.Request.Cookies.ContainsKey("UsuarioId"))
    {
        context.Session.SetInt32("UsuarioId", int.Parse(context.Request.Cookies["UsuarioId"]));

        // Aqui, verifique se o cookie "NomeUsuario" existe antes de adicionar na sessão
        var nomeUsuario = context.Request.Cookies["NomeUsuario"];
        if (!string.IsNullOrEmpty(nomeUsuario))
        {
            context.Session.SetString("NomeUsuario", nomeUsuario);
        }
    }

    await next();
});


app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();