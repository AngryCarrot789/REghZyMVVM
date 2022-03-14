using System.Collections.Generic;

namespace REghZy.Collections.Tables {
    public class HashTable_WIP<R, C, V> {
        private readonly Dictionary<R, Dictionary<C, V>> table;

        public HashTable_WIP() {
            this.table = new Dictionary<R, Dictionary<C, V>>();
        }

        public V this[R row, C column] {
            get => this.table[row][column];
            set => GetColumn(row)[column] = value;
        }

        private Dictionary<C, V> GetColumn(R row) {
            Dictionary<C, V> dict;
            if (!this.table.TryGetValue(row, out dict)) {
                this.table[row] = dict = new Dictionary<C, V>();
            }

            return dict;
        }
    }
}