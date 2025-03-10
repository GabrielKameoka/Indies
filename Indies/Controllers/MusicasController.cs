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
        if (await _db.Musicas.AnyAsync(m => m.Nome == musicas.Nome && m.Artista == musicas.Artista && m.Link == musicas.Link))
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

    [HttpPost]
    public IActionResult Editar(MusicasModel musicas)
    {
        if (ModelState.IsValid)
        {
            _db.Musicas.Update(musicas);
            _db.SaveChanges();
            
            TempData["MensagemSucesso"] = "Música editada com sucesso!";
            
            return RedirectToAction("Index");
        }

        return View(musicas);
    }
    
    [HttpPost]
    public async Task<IActionResult> Deletar(int id)
    {
        var music = await _db.Musicas.FindAsync(id);

        if (music == null)
        {
            TempData["MensagemErro"] = "Música não encontrada.";
            return RedirectToAction("Index");
        }

        _db.Musicas.Remove(music);
        await _db.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Música excluída com sucesso.";
        return RedirectToAction("Index");
    }
    
}