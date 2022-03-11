using System;

namespace REghZy.Utils {
    public static class Arrays {
        public static T[] Copy<T>(this T[] array) {
            T[] arr = new T[array.Length];
            Array.Copy(array, 0, arr, 0, array.Length);
            return arr;
        }

        public static T[] CopyOf<T>(this T[] array, int length) {
            int copyLength = Math.Min(length, array.Length);
            T[] copy = new T[length];
            Array.Copy(array, 0, copy, 0, copyLength);
            return copy;
        }

        public static T[] CopyRange<T>(this T[] array, int start, int end) {
            int length = end - start;
            int copyLength = Math.Min(length, array.Length - start);
            T[] copy = new T[length];
            Array.Copy(array, start, copy, 0, copyLength);
            return copy;
        }

        public static T[] AddValue<T>(this T[] array, T value) {
            T[] arr = new T[array.Length + 1];
            Array.Copy(array, 0, arr, 0, array.Length);
            arr[array.Length] = value;
            return arr;
        }

        public static T[] InsertValue<T>(this T[] array, T value, int index) {
            T[] arr = new T[array.Length + 1];
            Array.Copy(array, 0, arr, 0, index);
            arr[index] = value;
            Array.Copy(array, index, arr, index + 1, array.Length - index);
            return arr;
        }

        public static T[] RemoveAt<T>(this T[] array, int index) {
            T[] arr = new T[array.Length - 1];
            Array.Copy(array, 0, arr, 0, index);
            Array.Copy(array, index + 1, arr, index, arr.Length - index);
            return arr;
        }

        /// <summary>
        /// Fills an array with the given value
        /// </summary>
        public static void Fill<T>(T[] array, T value) {
            for(int i = 0, len = array.Length; i < len; ++i) { // avoid constant ldfld, if the runtime is unoptimised
                array[i] = value;
            }
        }

        public static void Fill<T>(T[] array, int startIndex, int endIndex, T value) {
            for(int i = startIndex, end = Math.Min(array.Length, endIndex); i < end; ++i) {
                array[i] = value;
            }
        }

        public static void FillSize<T>(T[] array, int startIndex, int length, T value) {
            for(int i = startIndex, end = startIndex + length; i < end; ++i) {
                array[i] = value;
            }
        }
    }
}
