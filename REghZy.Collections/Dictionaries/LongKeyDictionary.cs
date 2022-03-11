using System;
using System.Collections;
using System.Collections.Generic;
using REghZy.Utils;

namespace REghZy.Collections.Dictionaries {
    /// <summary>
    /// A dictionary that uses a long as a key, while also removing all possibilities of hash collision
    /// </summary>
    /// <typeparam name="V">The type of value this map contains</typeparam>
    public class LongKeyDictionary<V> : IEnumerable<LKDEntry<V>> {
        private const long EMPTY_KEY = -9223372036854775808L;
        private const int BUCKET_SIZE = 4096;
        private readonly long[][] keys = new long[BUCKET_SIZE][];
        private readonly V[][] values = new V[BUCKET_SIZE][];
        private FlatMap<V> flat = new FlatMap<V>();
        private int count;

        /// <summary>
        /// Returns the number of elements in this dictionary
        /// </summary>
        public int Count => this.count;

        /// <summary>
        /// Gets or sets the value for the given key
        /// </summary>
        /// <exception cref="KeyNotFoundException">
        /// The key was not found while getting a value
        /// </exception>
        public V this[long key] {
            get => TryGet(key, out V value) ? value : throw new KeyNotFoundException(key.ToString());
            set => Put(key, value);
        }

        /// <summary>
        /// Creates a new long-key dictionary
        /// </summary>
        public LongKeyDictionary() {

        }

        /// <summary>
        /// Efficiently hashes a MSW and LSW
        /// </summary>
        /// <returns></returns>
        public static long PairToHash(int msw, int lsw) {
            return ((long)msw << 32) + (long)lsw - -2147483648L;
        }

        /// <summary>
        /// Converts a hash (see <see cref="PairToHash"/>) to the most significant word; high 4 bytes
        /// </summary>
        public static int HashToMSW(long l) {
            return (int)(l >> 32);
        }

        /// <summary>
        /// Converts a hash (see <see cref="PairToHash"/>) to the least significant word; lower 4 bytes
        /// </summary>
        public static int HashToLSW(long l) {
            return (int)l + -2147483648;
        }

        /// <summary>
        /// Whether this dictionary is empty or not
        /// </summary>
        /// <returns>
        /// True if there are 0 elements, otherwise false
        /// </returns>
        public bool IsEmpty() {
            return this.count == 0;
        }

        /// <summary>
        /// Checks if this dictionary contains the given key
        /// </summary>
        public bool ContainsKey(long key) {
            if (this.count == 0) {
                return false;
            }
            else if (this.flat.ContainsKey(key)) {
                return true;
            }
            else {
                int index = (int) (KeyIndex(key) & 4095L);
                long[] inner = this.keys[index];
                if (inner == null) {
                    return false;
                }
                else {
                    for (int i = 0, len = inner.Length; i < len; ++i) {
                        long innerKey = inner[i];
                        if (innerKey == EMPTY_KEY) {
                            return false;
                        }

                        if (innerKey == key) {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        /// <summary>
        /// Clears this dictionary
        /// </summary>
        public void Clear() {
            if (this.count != 0) {
                this.count = 0;
                for (int i = 0; i < BUCKET_SIZE; i++) {
                    this.keys[i] = null;
                    this.values[i] = null;
                }

                this.flat = new FlatMap<V>();
            }
        }

        /// <summary>
        /// Attempts to get a value with the given key
        /// </summary>
        /// <returns>
        /// True if an entry was found, where the value parameter will contain it. False if the key was not found
        /// </returns>
        public bool TryGet(long key, out V value) {
            if (this.count == 0) {
                value = default;
                return false;
            }
            else if (this.flat.TryGet(key, out value)) {
                return true;
            }
            else {
                int index = (int) (KeyIndex(key) & 4095L);
                long[] inner = this.keys[index];
                if (inner == null) {
                    return false;
                }
                else {
                    for (int i = 0, len = inner.Length; i < len; ++i) {
                        long innerKey = inner[i];
                        if (innerKey == EMPTY_KEY) {
                            return false;
                        }

                        if (innerKey == key) {
                            value = this.values[index][i];
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the value with the given key
        /// </summary>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">
        /// The dictionary did not contain the key, or the dictionary is empty
        /// </exception>
        public V Get(long key) {
            if (this.count == 0) {
                throw new KeyNotFoundException("Dictionary is empty");
            }
            else if (this.flat.TryGet(key, out V value)) {
                return value;
            }
            else {
                int index = (int) (KeyIndex(key) & 4095L);
                long[] inner = this.keys[index];
                if (inner == null) {
                    throw new KeyNotFoundException($"Key: {key}, Index: {index}");
                }
                else {
                    for (int i = 0, len = inner.Length; i < len; ++i) {
                        long innerKey = inner[i];
                        if (innerKey == EMPTY_KEY) {
                            throw new KeyNotFoundException($"Key: {key}, Index: {index}, Inner Key: {innerKey}");
                        }

                        if (innerKey == key) {
                            return this.values[index][i];
                        }
                    }

                    throw new KeyNotFoundException($"Key: {key}, Index: {index}");
                }
            }
        }

        /// <summary>
        /// Puts the given value with the given key
        /// </summary>
        /// <returns>
        /// True if an old value was replaced; false if a new entry was added
        /// </returns>
        public bool Put(long key, V value) {
            this.flat.Put(key, value);
            int index = (int) (KeyIndex(key) & 4095L);
            long[] innerKeys = this.keys[index];
            V[] innerValues = this.values[index];
            if (innerKeys == null) {
                this.keys[index] = innerKeys = new long[8];
                Arrays.Fill(innerKeys, EMPTY_KEY);
                this.values[index] = innerValues = new V[8];
                innerKeys[0] = key;
                innerValues[0] = value;
                ++this.count;
            }
            else {
                int i;
                for (i = 0; i < innerKeys.Length; ++i) {
                    if (innerKeys[i] == EMPTY_KEY) {
                        ++this.count;
                        innerKeys[i] = key;
                        innerValues[i] = value;
                        return false;
                    }

                    if (innerKeys[i] == key) {
                        innerKeys[i] = key;
                        innerValues[i] = value;
                        return true;
                    }
                }

                this.keys[index] = innerKeys = innerKeys.CopyOf(i << 1);
                Arrays.Fill(innerKeys, i, innerKeys.Length, EMPTY_KEY);
                this.values[index] = innerValues = innerValues.CopyOf(i << 1);
                innerKeys[i] = key;
                innerValues[i] = value;
                ++this.count;
            }

            return false;
        }

        /// <summary>
        /// Removes an entry with the given key
        /// </summary>
        /// <returns>
        /// True if something was removed. False if nothing was removed; key was not contained
        /// </returns>
        public bool Remove(long key) {
            this.flat.Remove(key);
            int index = (int) (KeyIndex(key) & 4095L);
            long[] inner = this.keys[index];
            if (inner == null) {
                return false;
            }
            else {
                for (int i = 0; i < inner.Length && inner[i] != EMPTY_KEY; ++i) {
                    if (inner[i] == key) {
                        ++i;

                        while (i < inner.Length && inner[i] != EMPTY_KEY) {
                            inner[i - 1] = inner[i];
                            this.values[index][i - 1] = this.values[index][i];
                            ++i;
                        }

                        inner[i - 1] = EMPTY_KEY;
                        this.values[index][i - 1] = default;
                        --this.count;
                        return true;
                    }
                }

                return false;
            }
        }

        private static long KeyIndex(long key) {
            key ^= (long) ((ulong) key >> 33);
            key *= -49064778989728563L;
            key ^= (long) ((ulong) key >> 33);
            key *= -4265267296055464877L;
            key ^= (long) ((ulong) key >> 33);
            return key;
        }

        private struct Enumerator : IEnumerator<LKDEntry<V>>, IEnumerator {
                private readonly LongKeyDictionary<V> map;
                private int count;
                private int index;
                private int innerIndex;
                private long currentKey;
                private V currentValue;

                public Enumerator(LongKeyDictionary<V> map) {
                    this.map = map;
                    this.index = 0;
                    this.currentKey = EMPTY_KEY;
                    this.currentValue = default;
                    this.count = 0;
                    this.innerIndex = 0;
                }

                public void Dispose() {
                    this.currentValue = default;
                }

                public bool MoveNext() {
                    if (this.count >= this.map.count) {
                        return false;
                    }

                    long[][] keys = this.map.keys;
                    ++this.count;
                    if (this.currentKey != EMPTY_KEY) {
                        ++this.innerIndex;
                    }

                    for(; this.index < keys.Length; ++this.index) {
                        if (keys[this.index] != null) {
                            if (this.innerIndex < keys[this.index].Length) {
                                long key = keys[this.index][this.innerIndex];
                                V value = this.map.values[this.index][this.innerIndex];
                                if (key != EMPTY_KEY) {
                                    this.currentKey = key;
                                    this.currentValue = value;
                                    return true;
                                }
                            }

                            this.innerIndex = 0;
                        }
                    }

                    return false;
                }

                public LKDEntry<V> Current => new LKDEntry<V>(this.currentKey, this.currentValue);

                object IEnumerator.Current => this.currentKey;

                void IEnumerator.Reset() {
                    this.index = 0;
                    this.currentKey = EMPTY_KEY;
                    this.currentValue = default;
                }
            }

        private class FlatMap<T> {
            private readonly T[][] flatLookup = new T[1024][];
            private readonly bool[][] containment = new bool[1024][];

            public FlatMap() {
                for (int i = 0; i < 1024; i++) {
                    this.flatLookup[i] = new T[1024];
                    this.containment[i] = new bool[1024];
                }
            }

            public void Put(long msw, long lsw, T value) {
                long acx = Math.Abs(msw);
                long acz = Math.Abs(lsw);
                if (acx < 512L && acz < 512L) {
                    this.flatLookup[(int)(msw + 512L)][(int)(lsw + 512L)] = value;
                    this.containment[(int) (msw + 512L)][(int) (lsw + 512L)] = true;
                }
            }

            public void Put(long key, T value) {
                Put(HashToMSW(key), HashToLSW(key), value);
            }

            public void Remove(long key) {
                Put(key, default);
            }

            public void Remove(long msw, long lsw) {
                Put(msw, lsw, default);
            }

            public bool ContainsKey(long msw, long lsw) {
                return Math.Abs(msw) < 512L && Math.Abs(lsw) < 512L && this.containment[(int) (msw + 512L)][(int) (lsw + 512L)];
            }

            public bool ContainsKey(long key) {
                long msw = HashToMSW(key);
                long lsw = HashToLSW(key);
                return Math.Abs(msw) < 512L && Math.Abs(lsw) < 512L && this.containment[(int) (msw + 512L)][(int) (lsw + 512L)];
            }

            public bool TryGet(long msw, long lsw, out T value) {
                long acx = Math.Abs(msw);
                long acz = Math.Abs(lsw);
                if (acx < 512L && acz < 512L && this.containment[(int) (msw + 512L)][(int) (lsw + 512L)]) {
                    value = this.flatLookup[(int) (msw + 512L)][(int) (lsw + 512L)];
                    return true;
                }
                else {
                    value = default;
                    return false;
                }
            }

            public bool TryGet(long key, out T value)  {
                return TryGet(HashToMSW(key), HashToLSW(key), out value);
            }
        }

        public IEnumerator<LKDEntry<V>> GetEnumerator() {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new Enumerator(this);
        }
    }
}