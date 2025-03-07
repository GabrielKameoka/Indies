using Microsoft.AspNetCore.Mvc;
using Indies.Context;
using Indies.Models;
using Azure.Core;


namespace Indies.Controllers;

public class MusicasController : Controller
{
    readonly private ApplicationDbContext _db;
    
    public MusicasController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        IEnumerable<MusicasModel> musicas = _db.Musicas;
        return View(musicas);
    }

    [HttpGet]
    public IActionResult Cadastrar(int? id)
    {
        List<string> categorias = new List<string> { "Rock", "Pop", "Eletrônica", "Clássica", "Outros" };
        ViewBag.Categorias = categorias;
        return View();
    }

    [HttpGet]
    public IActionResult Editar(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        MusicasModel musicas = _db.Musicas.Find(id);

        if (musicas == null)
        {
            return NotFound();
        }

        return View(musicas);
    }

    [HttpPost]
    public IActionResult Cadastrar(MusicasModel musicas)
    {
        if (ModelState.IsValid)
        {
            _db.Musicas.Add(musicas);
            _db.SaveChanges();
            
            TempData["MensagemSucesso"] = "Música cadastrada com sucesso!";
            
            return RedirectToAction("Index");
        }
        return View();
    }
    
}