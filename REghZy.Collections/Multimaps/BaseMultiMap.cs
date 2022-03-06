using System.Collections;
using System.Collections.Generic;

namespace REghZy.Collections.Multimaps {
    public abstract class BaseMultiMap<K, V> : IMultiMap<K, V> {
        public abstract ICollection<K> Keys { get; }

        public abstract ICollection<ICollection<V>> Values { get; }

        public abstract ICollection<V> this[K key] { get; }

        public abstract int Count { get; }

        public abstract bool IsEmpty { get; }

        public virtual ICollection<V> Get(K key) {
            return this[key];
        }

        public virtual void Put(K key, V value) {
            GetOrCreate(key).Add(value);
        }

        public virtual void PutAll(K key, IEnumerable<V> values) {
            ICollection<V> vals = GetOrCreate(key);
            foreach (V value in values) {
                vals.Add(value);
            }
        }

        public virtual void PutAll(IMultiMap<K, V> multiMap) {
            foreach (K key in multiMap.Keys) {
                PutAll(key, multiMap[key]);
            }
        }

        public abstract ICollection<V> RemoveAll(K key);

        public abstract bool Remove(K key, V value);

        public virtual bool Contains(K key, V value) {
            return ContainsKey(key) && this[key].Contains(value);
        }

        public abstract bool ContainsKey(K key);

        public virtual bool ContainsValue(V value) {
            foreach (K key in this.Keys) {
                if (this[key].Contains(value)) {
                    return true;
                }
            }

            return false;
        }

        public abstract int Size();

        public virtual int Size(K key) {
            return ContainsKey(key) ? this[key].Count : 0;
        }

        public abstract void Clear();

        public abstract IDictionary<K, ICollection<V>> ToMap();

        /// <summary>
        /// A method that gets, or creates, a value collection for the given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected abstract ICollection<V> GetOrCreate(K key);

        public abstract IEnumerator<KeyValuePair<K, ICollection<V>>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}