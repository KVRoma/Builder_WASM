using Builder_WASM.Shared.Models;

namespace Builder_WASM.Client.Services
{
    public interface IAuthenticationService
    {
        AuthenticateResponse User { get; }
        Task Initialize();
        Task Login(AuthenticateRequest username_password);
        Task Logout();
    }
}
