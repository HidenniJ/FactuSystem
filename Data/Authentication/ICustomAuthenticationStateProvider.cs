using FactuSystem.Data.Model;
using FactuSystem.Data.Response;
using Microsoft.AspNetCore.Components.Authorization;

namespace FactuSystem.Authentication
{
    public interface ICustomAuthenticationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task UpdateAuthenticationState(Usuario userData);
    }
}