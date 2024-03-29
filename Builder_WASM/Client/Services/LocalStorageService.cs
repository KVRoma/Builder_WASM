﻿using Microsoft.JSInterop;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace Builder_WASM.Client.Services
{
    public class LocalStorageService : ILocalStorageService
    {
		private readonly IJSRuntime jsruntime;
		private string _storage = "localStorage";
		public LocalStorageService(IJSRuntime jSRuntime)
		{
			jsruntime = jSRuntime;
		}
		public void AddStorage(string storage)
		{
			_storage = storage;
		}
		public async Task RemoveAsync(string key)
		{			
			await jsruntime.InvokeVoidAsync(_storage + ".removeItem", key).ConfigureAwait(false);
		}

		public async Task SetAsync<T>(string key, T value)
		{
			var json = JsonSerializer.Serialize(value);

			var compressedBytes = await Compressor.CompressBytesAsync(Encoding.UTF8.GetBytes(json));
			await jsruntime.InvokeVoidAsync(_storage + ".setItem", key, Convert.ToBase64String(compressedBytes)).ConfigureAwait(false);
		}

		public async Task<T> GetAsync<T>(string key)
		{
			var str = await jsruntime.InvokeAsync<string>(_storage + ".getItem", key).ConfigureAwait(false);
			if (str == null)
				return default!;
			var bytes = await Compressor.DecompressBytesAsync(Convert.FromBase64String(str));
			var value = Encoding.UTF8.GetString(bytes);

			return JsonSerializer.Deserialize<T>(value)!;
		}

		public async Task ClearAsync()
		{
			await jsruntime.InvokeVoidAsync(_storage + ".clear").ConfigureAwait(false);
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
