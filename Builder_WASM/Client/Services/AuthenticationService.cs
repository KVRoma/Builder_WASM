using Builder_WASM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Builder_WASM.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService _httpService;
        private ILocalStorageService _localStorageService;
        private NavigationManager _navigationManager;
        public AuthenticateResponse? User{ get; private set; }
        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {           
            User = await _localStorageService.GetAsync<AuthenticateResponse>("user");
        }

        public async Task Login(AuthenticateRequest data)
        {           
            var temp = await _httpService.PostAPI<AuthenticateResponse>("account/authenticate", data);
            User = temp.Response;
            await _localStorageService.SetAsync("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveAsync("user");
            _navigationManager.NavigateTo("/authenticate/login");
        }
    }
}
