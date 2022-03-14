using System.Collections.Generic;

namespace REghZy.Collections.Multimaps {
    public class LinkedListMutlimap<K, V> : BaseDictionaryMultiMap<K, V> {
        public LinkedListMutlimap(int initialKeySize) : base(initialKeySize) {
        }

        public LinkedListMutlimap(int initialKeySize, int initialValueCollectionSize) : base(initialKeySize, initialValueCollectionSize) {
        }

        public LinkedListMutlimap(IDictionary<K, ICollection<V>> map) : base(map) {
        }

        public LinkedListMutlimap(IMultiMap<K, V> map) : base(map) {

        }

        public override void PutAll(K key, IEnumerable<V> values) {
            LinkedList<V> vals = (LinkedList<V>) GetOrCreate(key);
            foreach (V value in values) {
                vals.AddLast(value);
            }
        }

        /// <summary>
        /// Removes the last value from the linked list for the give key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void RemoveLast(K key) {
            ((LinkedList<V>)this[key]).RemoveLast();
        }

        /// <summary>
        /// Removes the first value from the linked list for the give key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void RemoveFirst(K key) {
            ((LinkedList<V>)this[key]).RemoveFirst();
        }

        protected override ICollection<V> CreateCollection() {
            return new LinkedList<V>();
        }
    }
}