using System.ComponentModel.DataAnnotations;
using FactuSystem.Data.Response;

namespace FactuSystem.Data.Model;

public class Cliente
{
    [Key]
    public int Id { get; set; }
    public string Cedula { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }


    public ClienteResponse ToResponse()
        => new()
        {
            Id = Id,
            Cedula = Cedula,
            Nombre = Nombre,
            Apellidos = Apellidos,
            Telefono = Telefono,
            Direccion = Direccion,
        };
}
