using Indies.Models;
using Microsoft.EntityFrameworkCore;

namespace Indies.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<MusicasModel> Musicas { get; set; }
}