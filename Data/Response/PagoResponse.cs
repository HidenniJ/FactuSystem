namespace FactuSystem.Data.Response;

public class PagoResponse
{
    public int Id { get; set; }
    public int FacturaID { get; set; }
    public DateTime Fecha { get; set; }
    public double MontoPagado { get; set; }
    public string? Observacion { get; set; }
}