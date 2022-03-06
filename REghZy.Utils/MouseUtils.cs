using System.Runtime.InteropServices;

namespace REghZy.Utils {
    public class MouseUtils {
        #region Structs

        internal struct POINT_W32 {
            public int x;
            public int y;
        }

        #endregion

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref POINT_W32 pt);

        [DllImport("User32.dll")]
        internal static extern bool SetCursorPos(int x, int y);

        public static bool TryGetCursorW32(out int x, out int y) {
            POINT_W32 pt = new POINT_W32();
            if (GetCursorPos(ref pt)) {
                x = pt.x;
                y = pt.y;
                return true;
            }

            x = 0;
            y = 0;
            return false;
        }

        public static bool SetCursorPosW32(int x, int y) {
            return SetCursorPos(x, y);
        }
    }
}