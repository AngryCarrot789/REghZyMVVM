using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace REghZy.Utils {
    public static class CollectionUtils {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty<T>(this ICollection<T> enumerable) {
            return enumerable.Count == 0;
        }

        /// <summary>
        /// A linq-based function, that checks if the given enumerable source is empty
        /// <para>
        /// It may not check every single element, because it will return false once any element satisfies the given predicate
        /// </para>
        /// <para>
        /// This is equivalent to <code>source.Any(predicate)</code>, but i keep forgetting that method name, and i'm more used to java's isEmpty() method
        /// </para>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the given enumerable source, or the predicate, are null</exception>
        public static bool IsEmpty<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            // the hope is that, 99.999999999999999% of the time, neither of them will be null
            // so hopefully the CLR can do branch prediction and optimise this,
            // so that the final else statement is being executed while the above ones are being checked
            // at least, that's how i think branch prediction works in CLR. i think it works like that in machine code,
            // but then again, that's machine code, not CLR code (though CLR can get JIT compiled but anyway)
            if (source == null) {
                throw new ArgumentNullException(nameof(source), "Source enumerable cannot be null");
            }
            else if (predicate == null) {
                throw new ArgumentNullException(nameof(predicate), "The given predicate cannot be null");
            }
            else {
                foreach (T element in source) {
                    if (predicate(element)) {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}