using Builder_WASM.Shared.Models;

namespace Builder_WASM.Server.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<ResponseMessage> ChangePassword(AuthenticateRequestChangePassword model, string role);
        Task<ResponseMessage> ChangeLogin(AuthenticateRequestChangeLogin model, string role);
    }
}
