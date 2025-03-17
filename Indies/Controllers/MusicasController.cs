using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Indies.Context;
using Indies.Models;
using Microsoft.AspNetCore.Authorization;

namespace Indies.Controllers;

public class MusicasController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public MusicasController(ApplicationDbContext db, DbContextOptions<ApplicationDbContext> options)
    {
        _db = db;
        _options = options;
    }

    public IActionResult Index()
    {
        var musicas = _db.Musicas.Include(m => m.Usuario).ToList();
        return View(musicas);
    }


    [HttpGet]
    public IActionResult Editar(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }


        var musicas = _db.Musicas.Include(m => m.Usuario).FirstOrDefault(m => m.Id == id);

        if (musicas == null)
        {
            return NotFound();
        }

        ViewBag.Categorias = new List<string> { "Rock", "Pop", "Eletronica", "Classica", "Nacional", "Gospel" };
        return View(musicas);
    }


    [HttpGet]
    [Authorize]
    public IActionResult Cadastrar()
    {
        ViewBag.Categorias = new List<string> { "Rock", "Pop", "Eletronica", "Classica", "Nacional", "Gospel" };
        return View();
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Cadastrar(MusicasModel musicas)
    {
        var usuarioLogado = User.Identity.Name;
        var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == usuarioLogado);

        if (usuario == null)
        {
            TempData["MensagemErro"] = "Erro: Usuário não autenticado.";
            return RedirectToAction("Index", "Musicas");
        }

        musicas.UsuarioId = usuario.Id;
        Console.WriteLine($"Usuario ID para musica: {usuario.Id}");

        if (ModelState.IsValid)
        {
            try
            {
                _db.Musicas.Add(musicas);
                await _db.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Música cadastrada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar no banco de dados: {ex.Message}");
                TempData["MensagemErro"] = "Erro ao salvar a música. Tente novamente.";
                ViewBag.Categorias = new List<string> { "Rock", "Pop", "Eletronica", "Classica", "Nacional", "Gospel" };
                return View(musicas);
            }
        }


        _db.Musicas.Add(musicas);
        await _db.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Música cadastrada com sucesso!";
        return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> Editar(MusicasModel musicas, int id)
    {
        var usuarioLogado = User.Identity.Name;
        var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == usuarioLogado);

        if (usuario == null)
        {
            TempData["MensagemErro"] = "Erro: Usuário não autenticado.";
            return RedirectToAction("Index", "Musicas");
        }

        var musicaExistente = _db.Musicas.AsNoTracking().FirstOrDefault(m => m.Id == id);

        if (musicaExistente == null)
        {
            TempData["MensagemErro"] = "Erro: Música não encontrada.";
            return RedirectToAction("Index");
        }

        if (musicaExistente.UsuarioId != usuario.Id)
        {
            TempData["MensagemErro"] = "Erro: Você não tem autorização para editar esta música.";
            return RedirectToAction("Index");
        }

        musicas.UsuarioId = musicaExistente.UsuarioId;

        _db.Musicas.Update(musicas);
        await _db.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Música editada com sucesso.";
        return RedirectToAction("Index");
    }



    [HttpPost]
    public async Task<IActionResult> Deletar(int id)
    {
        var usuarioLogado = User.Identity.Name;
        var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == usuarioLogado);
        
        var musica = await _db.Musicas.FindAsync(id);

        if (musica == null)
        {
            TempData["MensagemErro"] = "Música não encontrada.";
            return RedirectToAction("Index");
        }
        
        var musicaExistente = _db.Musicas.AsNoTracking().FirstOrDefault(m => m.Id == id);
        
        if (musicaExistente.UsuarioId != usuario.Id)
        {
            TempData["MensagemErro"] = "Erro: Você não tem autorização para excluir esta música.";
            return RedirectToAction("Index");
        }

        _db.Musicas.Remove(musica);
        await _db.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Música excluída com sucesso.";
        return RedirectToAction("Index");
    }
}