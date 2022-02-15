using System;

namespace REghZy.Utils {
    public static class ObjectUtils {
        /// <summary>
        /// Copies the given blittable 
        /// </summary>
        /// <typeparam name="T">The blittable type</typeparam>
        /// <param name="value">The value to copy</param>
        /// <returns>
        /// A unique deep-clone (all bytes copied) of the given value
        /// </returns>
        public static T CopyObj<T>(T value) where T : unmanaged {
            unsafe {
                int size = sizeof(T);
                T t = new T();
                Buffer.MemoryCopy((byte*) &t, (byte*) &value, size, size);
                return t;
            }
        }
    }
}
