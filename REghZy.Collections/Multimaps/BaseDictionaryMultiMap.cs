using System.Collections;
using System.Collections.Generic;

namespace REghZy.Collections.Multimaps {
    public abstract class BaseDictionaryMultiMap<K, V> : BaseMultiMap<K, V> {
        private readonly Dictionary<K, ICollection<V>> map;
        protected readonly int initialValueCollectionSize;

        public override ICollection<K> Keys => this.map.Keys;

        public override ICollection<ICollection<V>> Values => this.map.Values;

        public override ICollection<V> this[K key] => this.map[key];

        public override int Count => this.map.Count;

        public override bool IsEmpty => this.map.Count == 0;

        protected BaseDictionaryMultiMap(int initialKeySize) {
            this.map = new Dictionary<K, ICollection<V>>(initialKeySize);
            this.initialValueCollectionSize = (initialKeySize > 32 ? 32 : (initialKeySize < 4 ? 4 : initialKeySize));
        }

        protected BaseDictionaryMultiMap(int initialKeySize, int initialValueCollectionSize) {
            this.map = new Dictionary<K, ICollection<V>>(initialKeySize);
            this.initialValueCollectionSize = initialValueCollectionSize;
        }

        protected BaseDictionaryMultiMap(IDictionary<K, ICollection<V>> map) {
            this.map = new Dictionary<K, ICollection<V>>(map.Count);
            foreach (K key in map.Keys) {
                ((List<V>) GetOrCreate(key)).AddRange(map[key]);
            }
        }

        protected BaseDictionaryMultiMap(IMultiMap<K, V> map) {
            foreach (K key in map.Keys) {
                ((List<V>) GetOrCreate(key)).AddRange(map[key]);
            }
        }

        public override ICollection<V> RemoveAll(K key) {
            ICollection<V> values = this.map[key];
            this.map.Remove(key);
            return values;
        }

        public override bool Remove(K key, V value) {
            return this.map[key].Remove(value);
        }

        public override bool Contains(K key, V value) {
            return this.map.ContainsKey(key) && this.map[key].Contains(value);
        }

        public override bool ContainsKey(K key) {
            return this.map.ContainsKey(key);
        }

        public override int Size() {
            return this.map.Count;
        }

        public override void Clear() {
            this.map.Clear();
        }

        public override IDictionary<K, ICollection<V>> ToMap() {
            Dictionary<K, ICollection<V>> map = new Dictionary<K, ICollection<V>>();
            Dictionary<K, ICollection<V>> m = this.map;
            foreach (K key in m.Keys) {
                map.Add(key, new List<V>(m[key]));
            }

            return map;
        }

        protected sealed override ICollection<V> GetOrCreate(K key) {
            ICollection<V> values;
            if (this.map.TryGetValue(key, out values)) {
                return values;
            }
            else {
                this.map[key] = values = CreateCollection();
                return values;
            }
        }

        protected abstract ICollection<V> CreateCollection();

        public override IEnumerator<KeyValuePair<K, ICollection<V>>> GetEnumerator() {
            return this.map.GetEnumerator();
        }
    }
}