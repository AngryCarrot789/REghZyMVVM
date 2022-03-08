using System;

namespace REghZy.MathsF {
    public struct Vector3d {
        public double x;
        public double y;
        public double z;

        public static Vector3d Zero => new Vector3d(0.0d);
        public static Vector3d One => new Vector3d(1.0d);
        public static Vector3d UnitX => new Vector3d(1.0d, 0.0d, 0.0d);
        public static Vector3d UnitY => new Vector3d(0.0d, 1.0d, 0.0d);
        public static Vector3d UnitZ => new Vector3d(0.0d, 0.0d, 1.0d);

        // Right-handed cartesian coordinate system, which OpenGL uses
        public static Vector3d Up       => new Vector3d( 0.0d,  1.0d,  0.0d);
        public static Vector3d Down     => new Vector3d( 0.0d, -1.0d,  0.0d);
        public static Vector3d Left     => new Vector3d( 1.0d,  0.0d,  0.0d);
        public static Vector3d Right    => new Vector3d(-1.0d,  0.0d,  0.0d);
        public static Vector3d Backward => new Vector3d( 0.0d,  0.0d,  1.0d);
        public static Vector3d Forward  => new Vector3d( 0.0d,  0.0d, -1.0d);

        public Vector3d(Vector3d vector) {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }

        public Vector3d(double all) {
            this.x = all;
            this.y = all;
            this.z = all;
        }

        public Vector3d(double x, double y, double z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double Dot(Vector3d b) {
            return this.x * b.x + this.y * b.y + this.z * b.z;
        }

        public double Mag() {
            return (double) Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        public double MagSq() {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        public double Distance(Vector3d o) {
            return (double) Math.Sqrt(DMath.Square(this.x - o.x) + DMath.Square(this.y - o.y) + DMath.Square(this.z - o.z));
        }

        public double DistanceSquared(Vector3d o) {
            return DMath.Square(this.x - o.x) + DMath.Square(this.y - o.y) + DMath.Square(this.z - o.z);
        }

        public double Angle(Vector3d vec) {
            return Math.Acos(Normalise().Dot(vec.Normalise()));
        }

        public Vector3d Midpoint(Vector3d other) {
            return new Vector3d((this.x + other.x) / 2, (this.y + other.y) / 2, (this.z + other.z) / 2);
        }

        public Vector3d Cross(Vector3d b) {
            return new Vector3d(this.y * b.z - this.z * b.y, this.z * b.x - this.x * b.z, this.x * b.y - this.y * b.x);
        }

        public double MagnitudeSquare() {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        public double Magnitude() {
            return (double) Math.Sqrt(MagnitudeSquare());
        }

        public Vector3d Normalise() {
            double mag = this.Magnitude();
            return new Vector3d(this.x / mag, this.y / mag, this.z / mag);
        }

        public Vector3d ClipMagnitude(double max) {
            if (max <= 0.0d) {
                return new Vector3d(0.0d);
            }

            double r = MagnitudeSquare() / (max * max);
            if (r > 1.0d) {
                Vector3d v = this;
                v /= Math.Sqrt(r);
                return new Vector3d(v.x, v.y, v.z);
            }

            return this;
        }

        public bool IsNDC() {
            return this.x > -1.0d && this.x < 1.0d && this.y > -1.0d && this.y < 1.0d && this.z > -1.0d && this.z < 1.0d;
        }

        public static Vector3d operator +(Vector3d a, Vector3d b) {
            return new Vector3d(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3d operator +(Vector3d a, double b) {
            return new Vector3d(a.x + b, a.y + b, a.z + b);
        }

        public static Vector3d operator +(double a, Vector3d b) {
            return new Vector3d(a + b.x, a + b.y, a + b.z);
        }

        public static Vector3d operator -(Vector3d a, Vector3d b) {
            return new Vector3d(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3d operator -(Vector3d a, double b) {
            return new Vector3d(a.x - b, a.y - b, a.z - b);
        }

        public static Vector3d operator -(double b, Vector3d a) {
            return new Vector3d(b - a.x, b - a.y, b - a.z);
        }

        public static Vector3d operator -(Vector3d v) {
            return new Vector3d(-v.x, -v.y, -v.z);
        }

        public static Vector3d operator *(Vector3d a, Vector3d b) {
            return new Vector3d(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3d operator *(double b, Vector3d a) {
            return new Vector3d(b * a.x, b * a.y, b * a.z);
        }

        public static Vector3d operator *(Vector3d a, double b) {
            return new Vector3d(a.x * b, a.y * b, a.z * b);
        }

        public static Vector3d operator /(Vector3d a, Vector3d b) {
            return new Vector3d(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Vector3d operator /(double a, Vector3d v) {
            return new Vector3d(a / v.x, a / v.y, a / v.z);
        }

        public static Vector3d operator /(Vector3d a, double b) {
            return new Vector3d(a.x / b, a.y / b, a.z / b);
        }

        public bool Equals(Vector3d other, double tolerance = 0.0001d) {
            return Math.Abs(this.x - other.x) < tolerance && Math.Abs(this.y - other.y) < tolerance && Math.Abs(this.z - other.z) < tolerance;
        }

        public override bool Equals(object obj) {
            return obj is Vector3d other && Equals(other);
        }

        public Vector3d Copy() {
            return new Vector3d(this.x, this.y, this.z);
        }

        public override string ToString() {
            return $"Vector3({Math.Round(this.x, 2)}, {Math.Round(this.y, 2)}, {Math.Round(this.x, 2)})";
        }
    }
}