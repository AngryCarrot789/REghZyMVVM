using System;
using System.Text;

namespace REghZy.Utils {
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
        public static string JSubstring(this string value, int startIndex, int endIndex) {
            return value.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Java's substring method. Simply calls <code>value.Substring(startIndex)</code>
        /// </summary>
        public static string JSubstring(this string value, int startIndex) {
            return value.Substring(startIndex);
        }

        /// <summary>
        /// Repeats the given string the given number of times, and returns a string of the repeated given string
        /// </summary>
        /// <param name="value">The string to repeat</param>
        /// <param name="count">The number of times to repeat (and therefore the length of the return string, multiplied by the length of the given string)</param>
        public static string Repeat(this string value, int count) {
            if (value == null) {
                throw new ArgumentNullException(nameof(value), "Value (to repeat) cannot be null");
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
            return new string(character, count);
            // char[] chars = new char[count];
            // for(int i = 0; i < count; i++) {
            //     chars[i] = character;
            // }
            // return new string(chars);
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

        public static bool IsEmpty(this string value) {
            return value == null || value.Length == 0;
        }

        public static string Append(this string str, string value) {
            return str + value;
        }

        /// <summary>
        /// Gets the remaining characters after the given value
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="value">The value (exclusive)</param>
        /// <param name="startIndex">The index to start searching at. 0 by default</param>
        /// <returns>
        /// Null if this string doesn't contain the given value(at or past the given start index).
        /// Otherwise it returns the remaining characters after the given value
        /// </returns>
        /// <exception cref="ArgumentNullException">If any argument is null</exception>
        public static string After(this string str, string value, int startIndex = 0) {
            if (str == null) {
                throw new ArgumentNullException(nameof(str), "String cannot be null");
            }
            else if (value == null) {
                throw new ArgumentNullException(nameof(value), "Search value cannot be null");
            }
            else {
                int index = str.IndexOf(value, startIndex);
                if (index == -1) {
                    return null;
                }

                return str.Substring(index + value.Length);
            }
        }

        /// <summary>
        /// Gets the characters leading up to the given value
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="value">The value (exclusive)</param>
        /// <param name="startIndex">The index to start searching at. 0 by default</param>
        /// <returns>
        /// Null if this string doesn't contain the given value (at or past the given start index).
        /// Otherwise it returns the characters leading up to the given value
        /// </returns>
        /// <exception cref="ArgumentNullException">If any argument is null</exception>
        public static string Before(this string str, string value, int startIndex = 0) {
            if (str == null) {
                throw new ArgumentNullException(nameof(str), "String cannot be null");
            }
            else if (value == null) {
                throw new ArgumentNullException(nameof(value), "Search value cannot be null");
            }
            else {
                int index = str.IndexOf(value, startIndex);
                if (index == -1) {
                    return null;
                }

                return str.Substring(0, index);
            }
        }

        /// <summary>
        /// Returns the characters between the first occourance of the given 2 values; a and b
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="a">Between this... (exclusive)</param>
        /// <param name="b">and this (exclusive)</param>
        /// <param name="startIndex">The index to start searching at. 0 by default</param>
        /// <returns>
        /// Null if this string doesn't contain a or b (at or past the given start index)
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Between(this string str, string a, string b, int startIndex = 0) {
            if (str == null) {
                throw new ArgumentNullException(nameof(str), "String cannot be null");
            }
            else if (a == null) {
                throw new ArgumentNullException(nameof(a), "A value cannot be null");
            }
            else if (b == null) {
                throw new ArgumentNullException(nameof(b), "B value cannot be null");
            }
            else {
                int indexA = str.IndexOf(a, startIndex);
                if (indexA == -1) {
                    return null;
                }

                int indexB = str.IndexOf(b, indexA + a.Length);
                if (indexB == -1) {
                    return null;
                }

                return str.JSubstring(indexA + a.Length, indexB);
            }
        }
    }
}
