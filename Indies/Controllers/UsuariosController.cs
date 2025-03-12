using System.Security.Claims;
using Indies.Context;
using Indies.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Indies.Controllers;

public class UsuariosController : Controller
{
    private readonly ApplicationDbContext _db;

    public UsuariosController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Cadastrar(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new UsuariosModel());
    }

    public IActionResult Entrar()
    {
        return View();
    }
    
    

    [HttpPost]
    public IActionResult Cadastrar(UsuariosModel usuarios)
    {
        
        
        if (ModelState.IsValid)
        {
            string senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(usuarios.Senha);
            Console.WriteLine($"Senha criptografada: {senhaCriptografada}");

            usuarios.Senha = senhaCriptografada;
            _db.Usuarios.Add(usuarios);
            _db.SaveChanges();

            HttpContext.Session.SetString("NomeUsuario", usuarios.Nome);

            string returnUrl = TempData["ReturnUrl"] as string;
            return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction("Index", "Home");
        }

        return View(usuarios);
    }
    
    

    [HttpPost]
    public async Task<IActionResult> Entrar(UsuariosModel usuarios)
    {
        var usuarioExistente = _db.Usuarios.FirstOrDefault(x => x.Email == usuarios.Email);

        if (usuarioExistente == null || !BCrypt.Net.BCrypt.Verify(usuarios.Senha, usuarioExistente.Senha))
        {
            ModelState.AddModelError(string.Empty, "Email ou senha incorretos");
            return View(usuarios);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuarioExistente.Id.ToString()),
            new Claim(ClaimTypes.Name, usuarioExistente.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, 
            ExpiresUtc = DateTime.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

        HttpContext.Session.SetInt32("UsuarioId", usuarioExistente.Id);
        HttpContext.Session.SetString("NomeUsuario", usuarioExistente.Nome);
        Response.Cookies.Append("UsuarioId", usuarioExistente.Id.ToString(), new CookieOptions { Expires = DateTime.UtcNow.AddDays(7) });

        return RedirectToAction("Index", "Home");
    }

    
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();

        foreach (var cookie in HttpContext.Request.Cookies.Keys)
        {
            HttpContext.Response.Cookies.Delete(cookie);
        }

        return RedirectToAction("Index", "Home");
    }


    
}