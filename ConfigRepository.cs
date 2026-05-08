using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace ConfigRepository
{
    /// <summary>
    /// ScriptableObject-backed config repository that keeps repository keys and config identities synchronized.
    /// </summary>
    /// <typeparam name="TKey">Type of the repository key.</typeparam>
    /// <typeparam name="TConfig">Type of the stored config wrapper.</typeparam>
    public abstract class ConfigRepository<TKey, TConfig> : ScriptableObject, IConfigProvider<TKey, TConfig>
        where TConfig : Config<TKey>
    {
        [SerializeField, SerializedDictionary("Config Key", "Config")]
        private SerializedDictionary<TKey, TConfig> _configs = new();

        /// <summary>
        /// Tries to get the config registered for the specified key.
        /// </summary>
        /// <param name="key">Lookup key.</param>
        /// <param name="config">Matched config when the key exists; otherwise <see langword="null"/>.</param>
        /// <returns><see langword="true"/> when the key exists; otherwise <see langword="false"/>.</returns>
        public bool TryGet(TKey key, out TConfig config)
        {
            EnsureConfigs();
            return _configs.TryGetValue(key, out config);
        }

        /// <summary>
        /// Adds or replaces a config for the specified key and synchronizes the hidden config identity.
        /// </summary>
        /// <param name="key">Repository key.</param>
        /// <param name="config">Config wrapper instance to store.</param>
        /// <returns><see langword="true"/> when the write succeeded; otherwise <see langword="false"/>.</returns>
        public bool TrySet(TKey key, TConfig config)
        {
            EnsureConfigs();

            if (config == null)
            {
                Debug.LogWarning($"[{GetType().Name}]: Attempted to set a null config for key '{key}'.");
                return false;
            }

            _configs[key] = config;
            config.SetId(key);
            MarkDirty();
            return true;
        }

        /// <summary>
        /// Removes a config for the specified key.
        /// </summary>
        /// <param name="key">Repository key.</param>
        /// <returns><see langword="true"/> when an entry was removed; otherwise <see langword="false"/>.</returns>
        public bool TryRemove(TKey key)
        {
            EnsureConfigs();
            if (!_configs.Remove(key))
            {
                return false;
            }

            MarkDirty();
            return true;
        }

        /// <summary>
        /// Clears all stored configs.
        /// </summary>
        public void Clear()
        {
            EnsureConfigs();
            if (_configs.Count == 0)
            {
                return;
            }

            _configs.Clear();
            MarkDirty();
        }

        private void OnEnable()
        {
            EnsureConfigs();
        }

        private void OnValidate()
        {
            EnsureConfigs();

            foreach (KeyValuePair<TKey, TConfig> pair in _configs)
            {
                pair.Value?.SetId(pair.Key);
            }
        }

        private void EnsureConfigs()
        {
            _configs ??= new SerializedDictionary<TKey, TConfig>();
        }

        private void MarkDirty()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}
