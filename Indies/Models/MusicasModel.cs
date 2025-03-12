using System.ComponentModel.DataAnnotations;

namespace Indies.Models;

public enum CategoriaMusica
{
    Rock,
    Pop,
    Eletronica,
    Classica,
    Nacional,
    Gospel
}

public class MusicasModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Artista é obrigatório.")]
    public string Artista { get; set; }

    [Required(ErrorMessage = "O campo Categoria é obrigatório.")]
    public CategoriaMusica Categoria { get; set; } 

    [Required(ErrorMessage = "O campo Lançamento é obrigatório.")]
    public DateTime Lancamento { get; set; } 

    [Required(ErrorMessage = "O campo Link é obrigatório.")]
    [Url(ErrorMessage = "O Link informado não é válido.")]
    public string Link { get; set; }

    public int? UsuarioId { get; set; }

    public UsuariosModel Usuario { get; set; }
}