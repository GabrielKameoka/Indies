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
        IEnumerable<MusicasModel> musicas = _db.Musicas.Include(m => m.Usuario);
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
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage); // Verifique os erros no console
            }
            return View(musicas);
        }

        if (await _db.Musicas.AnyAsync(m => m.Nome == musicas.Nome && m.Artista == musicas.Artista))
        {
            TempData["MensagemErro"] = "Esta música já existe no banco";
            return RedirectToAction("Cadastrar");
        }

        _db.Musicas.Add(musicas);
        await _db.SaveChangesAsync();
        TempData["MensagemSucesso"] = "Música cadastrada com sucesso!";
        return RedirectToAction("Index");
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