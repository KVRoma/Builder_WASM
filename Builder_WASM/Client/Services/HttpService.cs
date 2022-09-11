using Builder_WASM.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Builder_WASM.Client.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;        

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IConfiguration configuration
        )
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;           
        }

        public async Task<ResponseAPI<T>> GetAPI<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequestAPI<T>(request);
        }

        public async Task<ResponseAPI<T>> PostAPI<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequestAPI<T>(request);
        }

        public async Task<ResponseAPI<T>> PutAPI<T>(string uri,object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            
            return await sendRequestAPI<T>(request);
        }

        public async Task<ResponseAPI<T>> DeleteAPI<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await sendRequestAPI<T>(request);            
        }

        //*********************************************************************************************************************************
        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequest<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

            return await sendRequest<T>(request);
        }

        public async Task<T> Delete<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await sendRequest<T>(request);
        }





        // helper methods

        private async Task<ResponseAPI<T>> sendRequestAPI<T>(HttpRequestMessage request)
        {
            int code;
            bool status;
            string mess;

            // add jwt auth header if user is logged in and request is to the api url
            var user = await _localStorageService.GetAsync<AuthenticateResponse>("user");
            //var isApiUrl = !request.RequestUri?.IsAbsoluteUri;
            if (user != null) //&& isApiUrl 
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

            using var response = await _httpClient.SendAsync(request);
                     

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("/authenticate/login");
                return default!;
            }

            code = (int)response.StatusCode;
            status = response.IsSuccessStatusCode;
            mess = await response.Content.ReadAsStringAsync();

            ResponseAPI<T> res = new ResponseAPI<T>();

            res.StatusCode = code;
            res.IsSuccessStatusCode = status;
            res.Message = mess;            
            res.Response = (await response.Content.ReadFromJsonAsync<T>())!;
                        
            return res;
        }





        //******************************************************************************************************************

        // helper methods

        private async Task<T> sendRequest<T>(HttpRequestMessage request)
        {
           
            // add jwt auth header if user is logged in and request is to the api url
            var user = await _localStorageService.GetAsync<AuthenticateResponse>("user");
            //var isApiUrl = !request.RequestUri?.IsAbsoluteUri;
            if (user != null) //&& isApiUrl 
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

            using var response = await _httpClient.SendAsync(request);

            
            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("/authenticate/login");
                return default!;
            }
            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadFromJsonAsync<AuthenticateResponse>();
                throw new Exception(user?.Message);
            }
            

            return (await response.Content.ReadFromJsonAsync<T>())!;
            
        }
    }
}
