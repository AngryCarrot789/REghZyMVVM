using System.Diagnostics;

namespace REghZy.Utils {
    public class Time {
        /// <summary>
        /// This specifies how many ticks there are in 1 second. This usually never changes during
        /// the app's runtime. Though, it might change for different operating systems
        /// <para>
        /// If one were to call <see cref="GetSystemTicks"/>, then <see cref="System.Threading.Thread.Sleep(int)"/>
        /// for 1000ms, then <see cref="GetSystemTicks"/>, the interval will roughly equal to this field's value
        /// </para>
        /// </summary>
        public static readonly long TICK_PER_SECOND = Stopwatch.Frequency; // windows = 10,000,000

        public static readonly double TICK_PER_SECOND_D = Stopwatch.Frequency; // windows = 10000000.0d

        /// <summary>
        /// A multiplier for converting ticks to milliseconds
        /// <para>
        /// If one were to call <see cref="GetSystemMillis"/>, then <see cref="System.Threading.Thread.Sleep(int)"/>
        /// for 1000ms, then <see cref="GetSystemMillis"/>, the interval will roughly equal to 1,000
        /// </para>
        /// </summary>
        public static readonly long TICK_PER_MILLIS = TICK_PER_SECOND / 1000; // windows = 10,000

        /// <summary>
        /// A multiplier for converting ticks to milliseconds
        /// <para>
        /// If one were to call <see cref="GetSystemNanos"/>, then <see cref="System.Threading.Thread.Sleep(int)"/>
        /// for 1000ms, then <see cref="GetSystemNanos"/>, the interval will roughly equal to 1,000,000
        /// </para>
        /// </summary>
        public static readonly long TICK_PER_NANOS = TICK_PER_SECOND / 1000000; // windows = 10

        /// <summary>
        /// Gets the system's performance counter ticks
        /// </summary>
        public static long GetSystemTicks() {
            return Stopwatch.GetTimestamp();
        }

        /// <summary>
        /// Gets the system's performance counter ticks and converts them to nanoseconds
        /// </summary>
        public static long GetSystemNanos() {
            return Stopwatch.GetTimestamp() / TICK_PER_NANOS;
        }

        /// <summary>
        /// Gets the system's performance counter ticks and converts them to milliseconds
        /// </summary>
        public static long GetSystemMillis() {
            return Stopwatch.GetTimestamp() / TICK_PER_MILLIS;
        }

        /// <summary>
        /// Gets the system's performance counter ticks and converts them to seconds
        /// </summary>
        /// <returns></returns>
        public static long GetSystemSeconds() {
            return Stopwatch.GetTimestamp() / TICK_PER_SECOND;
        }

        /// <summary>
        /// Gets the system's performance counter ticks and converts them to decimal seconds
        /// </summary>
        /// <returns></returns>
        public static double GetSystemSecondsD() {
            return (double) Stopwatch.GetTimestamp() / TICK_PER_SECOND_D;
        }
    }
}