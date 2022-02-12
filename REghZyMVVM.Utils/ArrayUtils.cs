namespace REghZyMVVM.Utils {
    public static class ArrayUtils {
        public static T[] Copy<T>(this T[] array) {
            T[] arr = new T[array.Length];
            for(int i = 0, len = arr.Length; i < len; i++) { // avoid constant ldfld, if the runtime is unoptimised
                arr[i] = array[i];
            }

            return arr;
        }
    }
}
