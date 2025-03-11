using System.ComponentModel.DataAnnotations;

namespace Indies.Models;

public enum CategoriaMusica
{
    Rock,
    Pop,
    Eletrônica,
    Clássica,
    Internacional,
    Gospel
}

public class MusicasModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Digite o nome da música")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "Digite o nome do Artista")]
    public string Artista { get; set; }
    
    public CategoriaMusica Categoria { get; set; }
    
    [Url]
    [Required(ErrorMessage = "Cole o link da música")]
    public string Link { get; set; }
    
    [Required(ErrorMessage = "Digite a data do lançamento")]
    public DateOnly Lancamento { get; set; }

    public UsuariosModel Usuario { get; set; }
}