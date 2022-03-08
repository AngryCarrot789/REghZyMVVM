using REghZy.MathsF;

namespace REghzy.MathsF {
    public struct Quaternion {
        public float x;
        public float y;
        public float z;
        public float w;

        public Quaternion(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Quaternion(Quaternion q) {
            this.x = q.x;
            this.y = q.y;
            this.z = q.z;
            this.w = q.w;
        }

        public Quaternion(Vector3f euler) {
            this.x = euler.x;
            this.y = euler.y;
            this.z = euler.z;
            this.w = 0;
        }

        public Quaternion(Vector3f euler, float w) {
            this.x = euler.x;
            this.y = euler.y;
            this.z = euler.z;
            this.w = w;
        }

        public Quaternion(Vector4f vec4) {
            this.x = vec4.x;
            this.y = vec4.y;
            this.z = vec4.z;
            this.w = vec4.w;
        }

        public Vector3f RotateVector(Vector3f coordinate) {
            Quaternion me = new Quaternion(this);
            Quaternion qv = new Quaternion(coordinate);
            Quaternion qr = this * qv * me.MultiplicativeInverse();
            return new Vector3f(qr.x, qr.y, qr.z);
        }

        public Quaternion MultiplicativeInverse() {
            return Conjungate() * (1.0f / Normalised());
        }

        private float Normalised() {
            return FMath.Pow(w, 2) + FMath.Pow(x, 2) + FMath.Pow(y, 2) + FMath.Pow(z, 2);
        }

        public Quaternion Conjungate() {
            return new Quaternion(-x, -y, -z, w);
        }

        public static Quaternion ToQuaternion(Vector3f euler) {
            float yaw = euler.y;
            float roll = euler.z;
            float pitch = euler.x;
            float cy = FMath.Cos(yaw / 2.0f);
            float sy = FMath.Sin(yaw / 2.0f);
            float cp = FMath.Cos(pitch / 2.0f);
            float sp = FMath.Sin(pitch / 2.0f);
            float cr = FMath.Cos(roll / 2.0f);
            float sr = FMath.Sin(roll / 2.0f);
            return new Quaternion(
                sr * cp * cy - cr * sp * sy,
                cr * sp * cy + sr * cp * sy,
                cr * cp * sy - sr * sp * cy,
                cr * cp * cy + sr * sp * sy);
        }

        public static Vector3f ToEuler(Quaternion q) {
            Vector3f angles = new Vector3f();

            // roll (x-axis rotation)
            float sr_cp = 2.0f * (q.w * q.x + q.y * q.z);
            float cr_cp = 1.0f - 2.0f * (q.x * q.x + q.y * q.y);
            angles.z = FMath.Atan2(sr_cp, cr_cp);

            // pitch (y-axis rotation)
            float sp = 2.0f * (q.w * q.y - q.z * q.x);
            if (FMath.Abs(sp) >= 1.0f) {
                angles.x = FMath.CopySign(Maths.PI_HALF, sp); // use 90 degrees if out of range
            }
            else {
                angles.x = FMath.Asin(sp);
            }

            // yaw (z-axis rotation)
            float sy_cp = 2.0f * (q.w * q.z + q.x * q.y);
            float cy_cp = 1.0f - 2 * (q.y * q.y + q.z * q.z);
            angles.y = FMath.Atan2(sy_cp, cy_cp);

            return angles;
        }

        public static Quaternion operator *(Quaternion a, Quaternion b) {
            return new Quaternion(
                a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
                a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
                a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w,
                a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z
            );
        }

        public static Quaternion operator *(Quaternion a, float b) {
            return new Quaternion(a.x * b, a.y * b, a.z * b, a.w * b);
        }

        public static Quaternion AngleAxis(float rotationRads, Vector3f axis) {
            Vector3f rot = axis * FMath.Sin(rotationRads * 0.5f);
            return new Quaternion(rot.x, rot.y, rot.z, FMath.Cos(rotationRads * 0.5f));
        }
    }
}