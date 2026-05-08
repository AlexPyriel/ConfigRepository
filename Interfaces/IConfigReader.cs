namespace ConfigRepository
{
    /// <summary>
    /// Read-only access to a config repository.
    /// </summary>
    /// <typeparam name="TKey">Type of the lookup key.</typeparam>
    /// <typeparam name="TConfig">Type of the stored config wrapper.</typeparam>
    public interface IConfigReader<in TKey, TConfig>
        where TConfig : Config<TKey>
    {
        /// <summary>
        /// Tries to get a config for the specified key.
        /// </summary>
        /// <param name="key">Lookup key.</param>
        /// <param name="config">Matched config when the key exists; otherwise <see langword="null"/>.</param>
        /// <returns><see langword="true"/> when the key exists; otherwise <see langword="false"/>.</returns>
        bool TryGet(TKey key, out TConfig config);
    }
}
