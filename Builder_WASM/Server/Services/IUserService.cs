using Builder_WASM.Shared.Models;

namespace Builder_WASM.Server.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
}
