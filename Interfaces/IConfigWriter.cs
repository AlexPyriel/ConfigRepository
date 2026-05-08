namespace ConfigRepository
{
    /// <summary>
    /// Writable access to a config repository.
    /// </summary>
    /// <typeparam name="TKey">Type of the repository key.</typeparam>
    /// <typeparam name="TConfig">Type of the stored config wrapper.</typeparam>
    public interface IConfigWriter<in TKey, in TConfig>
        where TConfig : Config<TKey>
    {
        /// <summary>
        /// Adds or replaces a config for the specified key.
        /// </summary>
        /// <param name="key">Repository key.</param>
        /// <param name="config">Config wrapper instance to store.</param>
        /// <returns><see langword="true"/> when the write succeeded; otherwise <see langword="false"/>.</returns>
        bool TrySet(TKey key, TConfig config);

        /// <summary>
        /// Removes a config for the specified key.
        /// </summary>
        /// <param name="key">Repository key.</param>
        /// <returns><see langword="true"/> when an entry was removed; otherwise <see langword="false"/>.</returns>
        bool TryRemove(TKey key);

        /// <summary>
        /// Clears all stored configs.
        /// </summary>
        void Clear();
    }
}
