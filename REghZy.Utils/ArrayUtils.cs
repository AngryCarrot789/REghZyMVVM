using System;

namespace REghZy.Utils {
    public static class ArrayUtils {
        public static T[] Copy<T>(this T[] array) {
            T[] arr = new T[array.Length];
            for(int i = 0, len = arr.Length; i < len; i++) { // avoid constant ldfld, if the runtime is unoptimised
                arr[i] = array[i];
            }

            return arr;
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
    }
}
