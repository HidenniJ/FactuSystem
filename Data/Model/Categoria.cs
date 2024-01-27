using FactuSystem.Data.Response;
using System.ComponentModel.DataAnnotations;

namespace FactuSystem.Data.Model;

public class Categoria
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;

    public CategoriaResponse? ToResponse() => new()
    {
        Id = Id,
        Nombre = Nombre
    };
}
