using System;
using UnityEngine;

namespace ConfigRepository
{
    /// <summary>
    /// Base Unity-serializable config model with a hidden identity.
    /// </summary>
    /// <typeparam name="TKey">Type of the identity key synchronized by a repository or provider.</typeparam>
    [Serializable]
    public abstract class Config<TKey>
    {
        [SerializeField, HideInInspector]
        private TKey _id;

        /// <summary>
        /// Gets the read-only identity stored inside the config wrapper.
        /// </summary>
        public TKey Id => _id;

        /// <summary>
        /// Updates the hidden identity stored in the config wrapper.
        /// </summary>
        /// <param name="id">Repository key mirrored into the config wrapper.</param>
        internal void SetId(TKey id)
        {
            _id = id;
        }
    }
}
