using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Indies.Context;
using Indies.Models;
using Microsoft.AspNetCore.Authorization;

namespace Indies.Controllers;

public class MusicasController : Controller
{
    private readonly ApplicationDbContext _db;

    public MusicasController(ApplicationDbContext db)
    {
        _db = db;
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

        var musicas = _db.Musicas.Find(id);

        if (musicas == null)
        {
            return NotFound();
        }

        ViewBag.Categorias = new List<string> { "Rock", "Pop", "Eletrônica", "Clássica", "Nacional", "Gospel" };
        return View(musicas);
    }
    
    

    [HttpGet]
    [Authorize]
    public IActionResult Cadastrar()
    {
        ViewBag.Categorias = new List<string> { "Rock", "Pop", "Eletrônica", "Clássica", "Nacional", "Gospel" };
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
            return RedirectToAction("Index", "Home");
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
                ViewBag.Categorias = new List<string> { "Rock", "Pop", "Eletrônica", "Clássica", "Nacional", "Gospel" };
                return View(musicas);
            }
        }


        _db.Musicas.Add(musicas);
        await _db.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Música cadastrada com sucesso!";
        return RedirectToAction("Index");
    }



    [HttpPost]
    public async Task<IActionResult> Editar(MusicasModel musicas)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var musicaExistente = await _db.Musicas.FindAsync(musicas.Id);
                if (musicaExistente == null)
                {
                    TempData["MensagemErro"] = "Música não encontrada.";
                    return RedirectToAction("Index");
                }

                musicaExistente.Nome = musicas.Nome;
                musicaExistente.Artista = musicas.Artista;
                musicaExistente.Categoria = musicas.Categoria;
                musicaExistente.Lancamento = musicas.Lancamento;
                musicaExistente.Link = musicas.Link;

                _db.Musicas.Update(musicaExistente);
                await _db.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Música editada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar a música: {ex.Message}");
                TempData["MensagemErro"] = "Erro ao atualizar a música. Tente novamente.";
                return View(musicas);
            }
        }

        ViewBag.Categorias = new List<string> { "Rock", "Pop", "Eletrônica", "Clássica", "Nacional", "Gospel" };
        return View(musicas);
    }

    [HttpPost]
    public async Task<IActionResult> Deletar(int id)
    {
        var musica = await _db.Musicas.FindAsync(id);

        if (musica == null)
        {
            TempData["MensagemErro"] = "Música não encontrada.";
            return RedirectToAction("Index");
        }

        _db.Musicas.Remove(musica);
        await _db.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Música excluída com sucesso.";
        return RedirectToAction("Index");
    }
}
