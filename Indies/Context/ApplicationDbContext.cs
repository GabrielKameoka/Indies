using Indies.Models;
using Microsoft.EntityFrameworkCore;

namespace Indies.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<MusicasModel> Musicas { get; set; }  
    public DbSet<UsuariosModel> Usuarios { get; set; }  
    public DbSet<AdministradorModel> Administrador { get; set; }  
}