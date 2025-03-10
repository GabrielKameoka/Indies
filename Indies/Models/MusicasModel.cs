using System.ComponentModel.DataAnnotations;

namespace Indies.Models;

public enum CategoriaMusica
{
    Rock,
    Pop,
    Eletronica,
    Classica,
    Internacional,
    Gospel
}

public class MusicasModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Artista { get; set; }
    public CategoriaMusica Categoria { get; set; }
    [Url]
    public string Link { get; set; }
    public DateOnly Lancamento { get; set; }
}