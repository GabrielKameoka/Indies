using Microsoft.AspNetCore.Mvc;
using Indies.Context;
using Indies.Models;
using Azure.Core;


namespace Indies.Controllers;

public class MusicasController : Controller
{
    readonly ApplicationDbContext _db;

    public MusicasController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }
    
}