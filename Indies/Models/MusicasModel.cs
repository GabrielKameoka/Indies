namespace Indies.Models;

public class MusicasModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Artista { get; set; }
    public DateOnly Lancamento { get; set; }
}