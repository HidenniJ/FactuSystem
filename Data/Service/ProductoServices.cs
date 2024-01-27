using FactuSystem.Data.Context;
using FactuSystem.Data.Response;
using Microsoft.EntityFrameworkCore;

namespace FactuSystem.Data.Services;

public class ProductoServices : IProductoServices
{
    private readonly IMyDbContext context;

    public ProductoServices(IMyDbContext context)
    {
        this.context = context;
    }

    public async Task<Result<List<ProductoResponse>>> Consultar(string filtro)
    {
        try
        {
            var contactos = await context.Productos
                .Where(c =>
                    (c.Nombre)
                    .ToLower()
                    .Contains(filtro.ToLower()
                    )
                )
                .Select(c => c.ToResponse())
                .ToListAsync();
            return new Result<List<ProductoResponse>>()
            {
                Message = "Ok",
                Success = true,
                Data = contactos
            };
        }
        catch (Exception E)
        {
            return new Result<List<ProductoResponse>>
            {
                Message = E.Message,
                Success = false
            };
        }
    }

}

public interface IProductoServices
    {
        Task<Result<List<ProductoResponse>>> Consultar(string filtro);
    }