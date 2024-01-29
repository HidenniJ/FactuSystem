using FactuSystem.Data.Request;

namespace FactuSystem.Data.Response;

public class CategoriaResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;

    public CategoriaRequest ToRequest()
    {
        return new CategoriaRequest
        {
            Id = Id,
            Nombre = Nombre

        };
    }
}