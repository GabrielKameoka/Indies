using System.ComponentModel.DataAnnotations;

namespace Indies.Models;

public class MusicasModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Artista { get; set; }
    public string Categoria { get; set; }
    [Url]
    public string Link { get; set; }
    public DateOnly Lancamento { get; set; }
}