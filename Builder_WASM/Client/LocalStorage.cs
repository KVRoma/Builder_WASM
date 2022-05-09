//Add in the "Program" file to string "builder.Services.AddSingleton<ILocalStorage, LocalStorage>();"
using Microsoft.JSInterop;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace Builder_WASM.Client
{
	public interface ILocalStorage
	{
		/// <summary>
		/// Remove a key from browser local storage.
		/// </summary>
		/// <param name="key">The key previously used to save to local storage.</param>
		public Task RemoveAsync(string key);

		/// <summary>
		/// Save a string value to browser local storage.
		/// </summary>
		/// <param name="key">The key to use to save to and retrieve from local storage.</param>
		/// <param name="value">The string value to save to local storage.</param>
		public Task SetAsync<T>(string key, T value);

		/// <summary>
		/// Get a string value from browser local storage.
		/// </summary>
		/// <param name="key">The key previously used to save to local storage.</param>
		/// <returns>The string previously saved to local storage.</returns>
		public Task<T> GetAsync<T>(string key);
		
		/// <summary>
		/// Clear value from browser local storage.
		/// </summary>
		/// <returns></returns>
		public Task ClearAsync();
	}

	public class LocalStorage : ILocalStorage
	{
		private readonly IJSRuntime jsruntime;
		public LocalStorage(IJSRuntime jSRuntime)
		{
			jsruntime = jSRuntime;
		}

		public async Task RemoveAsync(string key)
		{
			await jsruntime.InvokeVoidAsync("localStorage.removeItem", key).ConfigureAwait(false);
		}

		public async Task SetAsync<T>(string key, T value)
		{
			var json = JsonSerializer.Serialize(value);

			var compressedBytes = await Compressor.CompressBytesAsync(Encoding.UTF8.GetBytes(json));
            await jsruntime.InvokeVoidAsync("localStorage.setItem", key, Convert.ToBase64String(compressedBytes)).ConfigureAwait(false);           
        }

		public async Task<T> GetAsync<T>(string key)
		{
			var str = await jsruntime.InvokeAsync<string>("localStorage.getItem", key).ConfigureAwait(false);
            if (str == null)
                return default;
            var bytes = await Compressor.DecompressBytesAsync(Convert.FromBase64String(str));
			var value = Encoding.UTF8.GetString(bytes);

			return JsonSerializer.Deserialize<T>(value);
		}		

		public async Task ClearAsync()
		{
			await jsruntime.InvokeVoidAsync("localStorage.clear").ConfigureAwait(false);
		}
	}

	internal class Compressor
	{
		public static async Task<byte[]> CompressBytesAsync(byte[] bytes)
		{
			using (var outputStream = new MemoryStream())
			{
				using (var compressionStream = new GZipStream(outputStream, CompressionLevel.Optimal))
				{
					await compressionStream.WriteAsync(bytes, 0, bytes.Length);
				}
				return outputStream.ToArray();
			}
		}

		public static async Task<byte[]> DecompressBytesAsync(byte[] bytes)
		{
			using (var inputStream = new MemoryStream(bytes))
			{
				using (var outputStream = new MemoryStream())
				{
					using (var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
					{
						await compressionStream.CopyToAsync(outputStream);
					}
					return outputStream.ToArray();
				}
			}
		}
	}
}
