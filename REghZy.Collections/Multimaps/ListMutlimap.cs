using System.Collections.Generic;

namespace REghZy.Collections.Multimaps {
    public sealed class ListMutlimap<K, V> : BaseDictionaryMultiMap<K, V> {
        public ListMutlimap(int initialKeySize) : base(initialKeySize) {
        }

        public ListMutlimap(int initialKeySize, int initialValueCollectionSize) : base(initialKeySize, initialValueCollectionSize) {
        }

        public ListMutlimap(IDictionary<K, ICollection<V>> map) : base(map) {
        }

        public ListMutlimap(IMultiMap<K, V> map) : base(map) {
        }

        protected override ICollection<V> CreateCollection() {
            return new List<V>(this.initialValueCollectionSize);
        }
    }
}