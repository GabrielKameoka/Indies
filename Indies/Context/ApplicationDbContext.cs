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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MusicasModel>()
            .HasOne(m => m.Usuario)
            .WithMany()  
            .HasForeignKey(m => m.UsuarioId);
    }
}