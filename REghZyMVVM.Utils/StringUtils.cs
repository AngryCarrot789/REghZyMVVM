using System;
using System.Text;

namespace REghZyMVVM.Utils {
    /// <summary>
    /// Helper methods for strings
    /// </summary>
    public static class StringUtils {
        /// <summary>
        /// Java's substring, where you provide a start (inclusive) and end (exclusive) index
        /// </summary>
        /// <param name="value">The value to substring from</param>
        /// <param name="startIndex">Index of the first character to substring (inclusive)</param>
        /// <param name="endIndex">Index after the last character to substring (therefore, exclusive)</param>
        public static string JSubString(this string value, int startIndex, int endIndex) {
            return value.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Java's substring method. Simply calls <code>value.Substring(startIndex)</code>
        /// </summary>
        public static string JSubString(this string value, int startIndex) {
            return value.Substring(startIndex);
        }

        /// <summary>
        /// Repeats the given string the given number of times, and returns a string of the repeated given string
        /// </summary>
        /// <param name="value">The string to repeat</param>
        /// <param name="count">The number of times to repeat (and therefore the length of the return string, multiplied by the length of the given string)</param>
        public static string Repeat(this string value, int count) {
            if (value == null) {
                throw new ArgumentNullException("value", "Value (to repeat) cannot be null");
            }

            StringBuilder sb = new StringBuilder(count * value.Length);
            for(int i = 0; i < count; i++) {
                sb.Append(value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Repeats the given character the given number of times, and returns a string of the repeated characters
        /// </summary>
        /// <param name="character">The character to repeat</param>
        /// <param name="count">The number of times to repeat (and therefore the length of the return string)</param>
        public static string Repeat(this char character, int count) {
            char[] chars = new char[count];
            for(int i = 0; i < count; i++) {
                chars[i] = character;
            }

            return new string(chars);
        }

        /// <summary>
        /// Extracts a string between and at the given start and end index
        /// <para>
        /// Equivalent to <code>value.Substring(start, end - start - 1)</code>
        /// </para>
        /// </summary>
        /// <param name="value">The value to substring from</param>
        /// <param name="startIndex">First index in the string to extract from (inclusive)</param>
        /// <param name="endIndex">Last index in the string to extract from (inclusive)</param>
        public static string Extract(this string value, int startIndex, int endIndex) {
            return value.Substring(startIndex, endIndex - startIndex - 1);
        }
    }
}
