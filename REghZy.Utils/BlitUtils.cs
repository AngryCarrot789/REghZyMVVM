using System.Runtime.InteropServices;

namespace REghZy.Utils {
    public static class BlitUtils {
        /// <summary>
        /// Writes the given string to the given pointer
        /// </summary>
        /// <param name="ptr">A pointer to an array of characters</param>
        /// <param name="value">The string to write into the given pointer (ensure the destination pointer is big enough!)</param>
        public static unsafe void LoadString(char* ptr, string value) {
            fixed (char* strPtr = value) {
                Copy((byte*) strPtr, (byte*) ptr, (uint) value.Length * 2);
            }
        }

        public static unsafe void Copy(byte* src, byte* dest, uint length) {
            if (length >= 256) {
                CopyB256(src, dest, length);
            }
            else {
                CopyB64(src, dest, length);
            }
        }

        /// <summary>
        /// Copies 'length' number of bytes, from the given source, to the given destination
        /// <para>
        /// This initially copies in blocks of 64 bytes, then blocks of 16, then the remaining bytes (less than 16) are copied individually
        /// </para>
        /// </summary>
        /// <param name="dest">Source of the bytes to copy from</param>
        /// <param name="src">Destination of the bytes to paste into</param>
        /// <param name="length">The number of bytes to copy from src to dest</param>
        public static unsafe void CopyB64(byte* src, byte* dest, uint length) {
            uint len = length;
            while (len >= 64) {
                *(B64*) dest = *(B64*) src;
                dest += 64;
                src += 64;
                len -= 64;
            }
            
            while (len >= 16) {
                *(B64*) dest = *(B64*) src;
                dest += 16;
                src += 16;
                len -= 16;
            }

            while (len > 0) {
                *dest++ = *src++;
                --len;
            }
        }

        /// <summary>
        /// Copies 'length' number of bytes, from the given source, to the given destination. This method
        /// is intended for copying very large amounts of data, in the 1000s of bytes, for the reason below
        /// <para>
        /// This initially copies in blocks of 256 bytes, then blocks of 64, then blocks of 16, then the remaining bytes (less than 16) are copied individually
        /// </para>
        /// </summary>
        /// <param name="dest">Source of the bytes to copy from</param>
        /// <param name="src">Destination of the bytes to paste into</param>
        /// <param name="length">The number of bytes to copy from src to dest</param>
        public static unsafe void CopyB256(byte* src, byte* dest, uint length) {
            uint len = length;
            while (len >= 256) {
                *(B256*) dest = *(B256*) src;
                dest += 256;
                src += 256;
                len -= 256;
            }

            while (len >= 64) {
                *(B64*) dest = *(B64*) src;
                dest += 64;
                src += 64;
                len -= 64;
            }

            while (len >= 16) {
                *(B16*) dest = *(B16*) src;
                dest += 16;
                src += 16;
                len -= 16;
            }

            while (len > 0) {
                *dest++ = *src++;
                --len;
            }
        }

        [StructLayout(LayoutKind.Sequential, Size = 16)]
        private unsafe struct B16 { }

        [StructLayout(LayoutKind.Sequential, Size = 64)]
        private unsafe struct B64 { }

        [StructLayout(LayoutKind.Sequential, Size = 256)]
        private unsafe struct B256 { }
    }
}
