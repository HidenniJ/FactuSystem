namespace FactuSystem.Data.Response;

public class ClienteResponse
{
    public int Id { get; set; }
    public string Cedula { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
}