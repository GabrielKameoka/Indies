namespace Indies.Models;

public class AdministradorModel : UsuariosModel
{
    public Guid Chave { get; set; } = new Guid();
}