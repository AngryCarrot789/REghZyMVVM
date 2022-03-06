using System.Collections.Generic;

namespace REghZy.Collections.Multimaps {
    public sealed class HashSetMutlimap<K, V> : BaseDictionaryMultiMap<K, V> {
        public HashSetMutlimap(int initialKeySize) : base(initialKeySize) { }

        public HashSetMutlimap(int initialKeySize, int initialValueCollectionSize) : base(initialKeySize, initialValueCollectionSize) { }

        public HashSetMutlimap(IDictionary<K, ICollection<V>> map) : base(map) { }

        public HashSetMutlimap(IMultiMap<K, V> map) : base(map) { }

        protected override ICollection<V> CreateCollection() {
            return new HashSet<V>();
        }
    }
}