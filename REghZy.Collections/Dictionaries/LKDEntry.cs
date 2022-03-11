namespace REghZy.Collections.Dictionaries {
    /// <summary>
    /// An entry for the long-key dictionary
    /// </summary>
    /// <typeparam name="V">Value type</typeparam>
    public readonly struct LKDEntry<V> {
        public readonly long key;
        public readonly V value;

        public LKDEntry(long key, V value) {
            this.key = key;
            this.value = value;
        }
    }
}