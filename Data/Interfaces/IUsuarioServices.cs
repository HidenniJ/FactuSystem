using FactuSystem.Data.Services;
using FactuSystem.Data.Model;
using FactuSystem.Data.Request;
using FactuSystem.Data.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace FactuSystem.Data.Services
{
    public interface IUsuarioServices
    {
        Usuario? GetByUserName(string userName);
        Task CrearUsuarioAdmin();
        Task<Result> Crear(UsuarioRequest user);
        Task<Result> Modificar(UsuarioRequest request);
        Task<Result> Eliminar(UsuarioRequest request);
        Task<Result<List<UsuarioResponse>>> Consultar(string filtro);
        Task<Usuario> Autenticar(string username, string password);
        Task<Usuario> ObtenerUsuarioActual();
        Task<Usuario> ObtenerUsuarioPorId(int usuarioId);
        
    }
}