using FactuSystem.Data.Request;
using FactuSystem.Data.Response;
using System.ComponentModel.DataAnnotations;

namespace FactuSystem.Data.Model;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Role { get; set; }
public static Usuario Crear(UsuarioRequest usuario) =>
          new Usuario
          {
              Id = usuario.Id,
              Email = usuario.Email,
              Password = usuario.Password,
              Role = usuario.Role,
              Nombre = usuario.Nombre,
              Apellidos = usuario.Apellidos,
             
          };

public bool Modificar(UsuarioRequest usuario)
{
    var cambio = false;

    if (Password != usuario.Password)
    {
        Password = usuario.Password;
        cambio = true;
    }

    // No se modifican los otros campos en este método

    return cambio;
}
public UsuarioResponse ToResponse() =>
    new()
    {
        Id = Id,
        Email = Email,
        Password = Password,
        Role = Role,
        Nombre = Nombre,
        Apellidos = Apellidos,
       
    };
public UsuarioRequest ToRequest()
{
    return new UsuarioRequest
    {
        Id = Id,
        Email = Email,
        Password = Password,
        Role = Role,
        Nombre = Nombre,
        Apellidos = Apellidos,
        
    };


}

}
    