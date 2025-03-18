using System.ComponentModel.DataAnnotations;

namespace Indies.Models;

public class UsuariosModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Digite seu nome")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Digite seu e-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Digite sua senha")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    public ICollection<MusicasModel> Musicas { get; set; } = new List<MusicasModel>();

    //O ICollection<MusicasModel> Musicas indica um relacionamento "um-para-muitos"
    //entre UsuariosModel e MusicasModel. Isso significa que um usuário pode ter muitas
    //músicas, mas cada música pertence a um único usuário.
}