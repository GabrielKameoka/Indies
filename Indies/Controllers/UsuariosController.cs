using Indies.Context;
using Indies.Models;
using BCrypt.Net;
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
    public IActionResult Entrar(UsuariosModel usuarios)
    {
        var usuarioExistente = _db.Usuarios.FirstOrDefault(x => x.Email == usuarios.Email);

        if (usuarioExistente == null || !BCrypt.Net.BCrypt.Verify(usuarios.Senha, usuarioExistente.Senha))
        {
            ModelState.AddModelError(string.Empty, "Email ou senha incorretos");
            return View(usuarios);
        }

        HttpContext.Session.SetInt32("UsuarioId", usuarioExistente.Id);
        HttpContext.Session.SetString("NomeUsuario", usuarioExistente.Nome);

        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7),
            HttpOnly = true,
            IsEssential = true
        };

        Response.Cookies.Append("UsuarioId", usuarioExistente.Id.ToString(), cookieOptions);
        Response.Cookies.Append("NomeUsuario", usuarioExistente.Nome, cookieOptions);

        return RedirectToAction("Index", "Home");
    }




    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        Response.Cookies.Delete("UsuarioId");
        Response.Cookies.Delete("NomeUsuario");

        return RedirectToAction("Index", "Home");
    }

    
}