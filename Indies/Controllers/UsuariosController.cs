using Indies.Context;
using Indies.Models;
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

    [HttpPost]
    public IActionResult Cadastrar(UsuariosModel usuarios)
    {
        bool logado = false;
        if (ModelState.IsValid)
        {
            _db.Usuarios.Add(usuarios);
            _db.SaveChanges();
            
            logado = true;
            HttpContext.Session.SetString("NomeUsuario", usuarios.Nome);
            
            string returnUrl = (string)TempData["ReturnUrl"];

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        ViewBag.Logado = logado;
        return View(usuarios);
    }
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
    
}