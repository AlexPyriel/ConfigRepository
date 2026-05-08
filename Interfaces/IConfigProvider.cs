namespace ConfigRepository
{
    /// <summary>
    /// Combined read/write config repository contract.
    /// </summary>
    /// <typeparam name="TKey">Type of the repository key.</typeparam>
    /// <typeparam name="TConfig">Type of the stored config wrapper.</typeparam>
    public interface IConfigProvider<in TKey, TConfig> : IConfigReader<TKey, TConfig>, IConfigWriter<TKey, TConfig>
        where TConfig : Config<TKey> { }
}
