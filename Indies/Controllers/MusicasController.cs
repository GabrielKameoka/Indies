using Microsoft.AspNetCore.Mvc;
using Indies.Context;
using Indies.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;


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
        List<string> categorias = new List<string> { "Rock", "Pop", "Eletrônica", "Clássica", "Internacional","Gospel" };
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
    public async Task<IActionResult> Cadastrar(MusicasModel musicas)
    {
        if (await _db.Musicas.AnyAsync(m => m.Nome == musicas.Nome && m.Artista == musicas.Artista))
        {
            TempData["MensagemErro"] = "Música já foi cadastrada";
        
            return RedirectToAction("Index");
        }
        
        if (ModelState.IsValid)
        {
            _db.Musicas.Add(musicas);
            await _db.SaveChangesAsync();
        
            TempData["MensagemSucesso"] = "Música cadastrada com sucesso!";
        
            return RedirectToAction("Index");
        }
        
        return View();
    }
    
}