namespace REghZy.MathsF {
    public class Maths {
        public const float PI_HALF      = 1.57079632679489f;
        public const float PI           = 3.14159265358979f;
        public const float PI_DOUBLE    = 6.28318530717958f;
        private const float R2D = 180.0f / PI;
        private const float D2R = PI / 180.0f;

        public static float RadiansToDegrees(float radians) => radians * R2D;

        public static float DegreesToRadians(float degrees) => degrees * D2R;
    }
}