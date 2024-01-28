using FactuSystem.Data.Context;
using FactuSystem.Data.Model;
using FactuSystem.Data.Response;
using FactuSystem.Data.Request;
using Microsoft.EntityFrameworkCore;

namespace FactuSystem.Data.Services;

public class ProductoServices : IProductoServices
{
    private readonly IMyDbContext dbContext;

    public ProductoServices(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<List<ProductoResponse>>> Consultar(string filtro)
    {
        try
        {
            var contactos = await dbContext.Productos
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
                Mensaje = "Ok",
                Exitoso = true,
                Datos = contactos
            };
        }
        catch (Exception E)
        {
            return new Result<List<ProductoResponse>>
            {
                Mensaje = E.Message,
                Exitoso = false
            };
        }
    }

    public async Task<Result> Crear(ProductoRequest request)
    {
        try
        {
            var contacto = Producto.Crear(request);
            dbContext.Productos.Add(contacto);
            await dbContext.SaveChangesAsync();
            return new Result() { Mensaje = "Ok", Exitoso = true };
        }
        catch (Exception E)
        {

            return new Result() { Mensaje = E.Message, Exitoso = false };
        }
    }
    public async Task<Result> Modificar(ProductoRequest request)
    {
        try
        {
            var contacto = await dbContext.Productos
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (contacto == null)
                return new Result() { Mensaje = "No se encontro el proveedor", Exitoso = false };

            if (contacto.Mofidicar(request))
                await dbContext.SaveChangesAsync();

            return new Result() { Mensaje = "Ok", Exitoso = true };
        }
        catch (Exception E)
        {

            return new Result() { Mensaje = E.Message, Exitoso = false };
        }
    }

    public async Task<Result> Eliminar(ProductoRequest request)
    {
        try
        {
            var contacto = await dbContext.Productos
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (contacto == null)
                return new Result() { Mensaje = "No se encontro el producto", Exitoso = false };

            dbContext.Productos.Remove(contacto);
            await dbContext.SaveChangesAsync();
            return new Result() { Mensaje = "Ok", Exitoso = true };
        }
        catch (Exception E)
        {

            return new Result() { Mensaje = E.Message, Exitoso = false };
        }
    }
}

public interface IProductoServices
{
    Task<Result<List<ProductoResponse>>> Consultar(string filtro);
    Task<Result> Crear(ProductoRequest request);
    Task<Result> Modificar(ProductoRequest request);
    Task<Result> Eliminar(ProductoRequest request);
}