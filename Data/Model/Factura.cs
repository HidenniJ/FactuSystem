using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using FactuSystem.Data.Request;
using FactuSystem.Data.Response;

namespace FactuSystem.Data.Model;

public class Factura
{
    [Key]
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTime Fecha { get; set; }
    public virtual ICollection<FacturaDetalle> Detalles { get; set; }
    #region Relaciones
    [ForeignKey(nameof(ClienteId))]
    public virtual Cliente Cliente { get; set; }
    #endregion

    [NotMapped]
    public decimal SubTotal =>
        Detalles != null ? //IF
        Detalles.Sum(d => d.SubTotal) //Verdadero
        :
        0;//Falso

    [NotMapped]
    public decimal Descuento =>
        Detalles != null ? //IF
        Detalles.Sum(d => d.TotalDesc) //Verdadero
        :
        0;//Falso

    public decimal SaldoPagado { get; set; }
    public decimal SaldoPendiente { get; set; }

    public static Factura Crear(FacturaRequest request)
        => new()
        {
            ClienteId = request.ClienteId,
            Fecha = DateTime.Now,
            Detalles = request.Detalles
            .Select(detalle=>FacturaDetalle.Crear(detalle))
            .ToList()
        };
    public FacturaResponse ToResponse()
        => new()
        {
            Id = Id,
            ClienteId = ClienteId,
            Fecha = Fecha,
            Cliente = Cliente.ToResponse(),
            Detalles = Detalles.Select(d => d.ToResponse()).ToList()
        };
}
