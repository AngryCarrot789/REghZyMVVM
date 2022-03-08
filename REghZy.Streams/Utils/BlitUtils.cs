using System.Runtime.InteropServices;

namespace REghZy.Streams.Utils {
    public static class BlitUtils {

        public static unsafe void BlitFlip(byte* b_in, byte* b_out, int size) {
            for (int i = 0; i < size; ++i) {
                b_out[i] = b_in[size - i - 1];
            }
        }

        [StructLayout(LayoutKind.Sequential, Size = 16)]
        private unsafe struct B16 { }
    }
}