using Builder_WASM.Shared.Models;

namespace Builder_WASM.Server.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
