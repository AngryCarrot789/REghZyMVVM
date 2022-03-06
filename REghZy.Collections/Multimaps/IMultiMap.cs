using System.Collections;
using System.Collections.Generic;

namespace REghZy.Collections.Multimaps {
    public interface IMultiMap<K, V> : IEnumerable<KeyValuePair<K, ICollection<V>>> {
        /// <summary>
        /// Returns a collection of keys that this multimap contains
        /// </summary>
        ICollection<K> Keys { get; }

        /// <summary>
        /// Returns a collection of collections that this multimap contains
        /// </summary>
        ICollection<ICollection<V>> Values { get; }

        /// <summary>
        /// Returns a collection of values for the given key
        /// </summary>
        /// <param name="key">The key</param>
        /// <exception cref="KeyNotFoundException">The multimap does not contain the key</exception>
        ICollection<V> this[K key] { get; }

        /// <summary>
        /// Returns the size of this multimap (aka the number of keys)
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Indicates whether this multimap is empty or not. This is equivalent to
        /// <code>
        /// this.Count == 0
        /// </code>
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Returns a collection of values for the given key
        /// <para>
        /// This is a normal method equivalent to <see cref="IMultiMap{K,V}.this"/>
        /// </para>
        /// </summary>
        /// <param name="key">The key</param>
        /// <exception cref="KeyNotFoundException">The multimap does not contain the key</exception>
        ICollection<V> Get(K key);

        /// <summary>
        /// Adds a value for the given key
        /// </summary>
        void Put(K key, V value);

        /// <summary>
        /// Adds all of the values for the given key
        /// </summary>
        void PutAll(K key, IEnumerable<V> values);

        /// <summary>
        /// Adds all of the keys and their values from the given multimap to this multimap
        /// </summary>
        void PutAll(IMultiMap<K, V> multiMap);

        /// <summary>
        /// Removes all of the values for the given key (and removes the key)
        /// </summary>
        /// <returns>
        /// The values that were removed
        /// </returns>
        ICollection<V> RemoveAll(K key);

        /// <summary>
        /// Removes the value for the given key
        /// </summary>
        bool Remove(K key, V value);

        /// <summary>
        /// Returns whether the given value is contained in this multimap for the given key
        /// </summary>
        bool Contains(K key, V value);

        /// <summary>
        /// Returns whether the given key is contained in this multimap
        /// </summary>
        bool ContainsKey(K key);

        /// <summary>
        /// Returns whether this multimap contains any trace of the given value, in any key
        /// </summary>
        bool ContainsValue(V value);

        /// <summary>
        /// The size of this multimap. This is the exact same as <see cref="Count"/>
        /// </summary>
        int Size();

        /// <summary>
        /// Returns the number of values there are for the given key, or 0 if the key doesn't exist
        /// </summary>
        int Size(K key);

        /// <summary>
        /// Clears this map. This will not clear any underlying collections, meaning they will be left open for garbage collection
        /// </summary>
        void Clear();

        /// <summary>
        /// Converts this multimap into a dictionary. The dictionary will not affect this multimap in any way
        /// </summary>
        IDictionary<K, ICollection<V>> ToMap();
    }
}