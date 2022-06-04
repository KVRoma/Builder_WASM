namespace Builder_WASM.Client.Services
{   
	public interface ILocalStorageService
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
		public void AddStorage(string storage);
	}
	
}
