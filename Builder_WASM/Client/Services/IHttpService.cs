using Builder_WASM.Shared.Models;

namespace Builder_WASM.Client.Services
{
    public interface IHttpService
    {
        Task<ResponseAPI<T>> GetAPI<T>(string uri);
        Task<ResponseAPI<T>> PostAPI<T>(string uri, object value);
        Task<ResponseAPI<T>> PutAPI<T>(string uri, object value);
        Task<ResponseAPI<T>> DeleteAPI<T>(string uri);

        Task<T> Get<T>(string uri);
        Task<T> Post<T>(string uri, object value);
        Task<T> Put<T>(string uri, object value);
        Task<T> Delete<T>(string uri);
    }
}
