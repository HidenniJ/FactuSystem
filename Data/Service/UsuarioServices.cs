using FactuSystem.Data.Services;
using FactuSystem.Data.Context;
using FactuSystem.Data.Model;
using FactuSystem.Data.Request;
using FactuSystem.Data.Response;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using FactuSystem.Data.Services;

namespace FactuSystem.Data.Services
{

    public class UsuarioServices : IUsuarioServices
    {
        private readonly MyDbContext dBContext;
        private readonly AuthenticationStateProvider _authStateProvider;


        public UsuarioServices(MyDbContext dBContext, AuthenticationStateProvider authStateProvider)
        {
            this.dBContext = dBContext;
            _authStateProvider = authStateProvider;
        }


        public async Task<Result> Crear(UsuarioRequest request)
        {
            try
            {
                var usuario = Usuario.Crear(request);
                dBContext.Usuarios.Add(usuario);
                await dBContext.SaveChangesAsync();
                return new Result { Success = true, Message = "OK" };
            }
            catch (Exception E)
            {
                return new Result { Success = false, Message = E.Message };
            }
        }

        public async Task<Result> Modificar(UsuarioRequest request)
        {
            try
            {
                var usuario = await dBContext.Usuarios
                    .FirstOrDefaultAsync(u => u.Id == request.Id);

                if (usuario == null)
                    return new Result { Success = false, Message = "No se encuentra el usuario" };

                usuario.Password = request.Password; // Cambiar solo la contraseña
                await dBContext.SaveChangesAsync();

                return new Result { Success = true, Message = "OK" };
            }
            catch (Exception E)
            {
                return new Result { Success = false, Message = E.Message };
            }
        }

        public async Task<Result> Eliminar(UsuarioRequest request)
        {
            try
            {
                var usuario = await dBContext.Usuarios
                    .FirstOrDefaultAsync(u => u.Id == request.Id);
                if (usuario == null)
                    return new Result { Success = false, Message = "No se encuentra el usuario" };
                dBContext.Usuarios.Remove(usuario);
                await dBContext.SaveChangesAsync();
                return new Result { Success = true, Message = "OK" };
            }
            catch (Exception E)
            {
                return new Result { Success = false, Message = E.Message };
            }
        }

        public async Task<Result<List<UsuarioResponse>>> Consultar(string filtro)
        {
            try
            {
                var contactos = await dBContext.Usuarios
                    .Where(u => (u.Email + " " + u.Password + " " + u.Role)
                    .ToLower()
                    .Contains(filtro.ToLower()))
                    .Select(u => u.ToResponse())
                    .ToListAsync();
                return new Result<List<UsuarioResponse>>
                { Message = "OK", Success = true, Data = contactos };
            }
            catch (Exception E)
            {
                return new Result<List<UsuarioResponse>>
                { Message = E.Message, Success = true };
            }
        }
        public async Task<Usuario> Autenticar(string username, string password)
        {
            var usuario = await dBContext.Usuarios.FirstOrDefaultAsync(u => u.Email == username && u.Password == password);
            return usuario!;
        }
        public async Task<Usuario> ObtenerUsuarioActual()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                var usuarioId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)); // Asumiendo que el ID del usuario está en NameIdentifier claim
                return await ObtenerUsuarioPorId(usuarioId);
            }

            return null;

        }

        public async Task<Usuario> ObtenerUsuarioPorId(int usuarioId)
        {
            var authenticationState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authenticationState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userId, out int userIdInt))
                {
                    // Aquí debes implementar la lógica para obtener el usuario por su ID
                    // desde tu base de datos o donde sea que estés almacenando los usuarios.
                    // Por ejemplo, puedes usar Entity Framework o algún otro mecanismo.
                    // Devuelve el usuario encontrado o null si no se encuentra.
                    // La siguiente línea es solo un ejemplo y debes ajustarla a tu código.
                    return await dBContext.Usuarios.FirstOrDefaultAsync(u => u.Id == userIdInt);
                }
            }

            return null; // No hay usuario autenticado o ID no válido.
        }
 


    }
}
