using FactuSystem.Data.Context;
using FactuSystem.Data.Model;
using FactuSystem.Data.Request;
using FactuSystem.Data.Response;
using Microsoft.EntityFrameworkCore;

namespace FactuSystem.Data.Services;

public class FacturaServices: IFacturaServices
{
    private readonly IMyDbContext dbContext;

    public FacturaServices(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<List<FacturaResponse>>> Consultar()
    {
        try
        {
            var facturas = await dbContext.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles)
                .ThenInclude(d => d.Producto)
                .Select(f => f.ToResponse())
                .ToListAsync();
            return new Result<List<FacturaResponse>>()
            {
                Data = facturas,
                Success = true,
                Message = "Ok"
            };
        }
        catch (Exception E)
        {
            return new Result<List<FacturaResponse>>()
            {
                Data = null,
                Success = false,
                Message = E.Message
            };
        }
    }

    public async Task<Result<FacturaResponse>> Crear(FacturaRequest request)
    {
        try
        {
            var factura = Factura.Crear(request);
            dbContext.Facturas.Add(factura);
            await dbContext.SaveChangesAsync();
            return new Result<FacturaResponse>()
            {
                Data = factura.ToResponse(),
                Success = true,
                Message = "Ok"
            };
        }
        catch (Exception E)
        {
            return new Result<FacturaResponse>()
            {
                Data = null,
                Success = false,
                Message = E.Message
            };
        }
    }
}

public interface IFacturaServices
    {
        Task<Result<List<FacturaResponse>>> Consultar();
        Task<Result<FacturaResponse>> Crear(FacturaRequest request);
    }