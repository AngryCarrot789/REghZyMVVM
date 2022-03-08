using System.Runtime.CompilerServices;

namespace REghZy.MathsF {
    public static class Bits {
        // This is copied from .NET Framework 4.6.1 BitConverter class,
        // because .NET Standard doesn't have 32-bit versions :(
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe long DoubleToInt64Bits(double value) {
            return *((long*) &value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe double Int64BitsToDouble(long value) {
            return *((double*) &value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int SingleToInt32Bits(float value) {
            return *((int*) &value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe float Int32BitsToSingle(int value) {
            return *((float*) &value);
        }
    }
}