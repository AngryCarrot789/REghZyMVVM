using System;
using System.Runtime.CompilerServices;

namespace REghZy.MathsF {
    public struct Matrix4 {
        private float M0; private float M1; private float M2; private float M3;
        private float M4; private float M5; private float M6; private float M7;
        private float M8; private float M9; private float MA; private float MB;
        private float MC; private float MD; private float ME; private float MF;

        // 0  1  2  3
        // 4  5  6  7
        // 8  9  A  B
        // C  D  E  F

        /// <summary>
        /// Gets a pointer to the first element in this matrix
        /// </summary>
        public unsafe float* Pointer => GetPointer(this);

        // public static unsafe float* GetPointer(Matrix4 m) => &m.M0;

        public static unsafe float* GetPointer(in Matrix4 matrix) {
            fixed (float* ptr = &matrix.M0)
                return ptr;
        }

        public float this[int i] {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                switch (i) {
                    case 0: return this.M0;
                    case 1: return this.M1;
                    case 2: return this.M2;
                    case 3: return this.M3;
                    case 4: return this.M4;
                    case 5: return this.M5;
                    case 6: return this.M6;
                    case 7: return this.M7;
                    case 8: return this.M8;
                    case 9: return this.M9;
                    case 10: return this.MA;
                    case 11: return this.MB;
                    case 12: return this.MC;
                    case 13: return this.MD;
                    case 14: return this.ME;
                    case 15: return this.MF;
                    default: throw new ArgumentOutOfRangeException(nameof(i), "Index must be between 0 and 15");
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                switch (i) {
                    case 0: this.M0 = value; return;
                    case 1: this.M1 = value; return;
                    case 2: this.M2 = value; return;
                    case 3: this.M3 = value; return;
                    case 4: this.M4 = value; return;
                    case 5: this.M5 = value; return;
                    case 6: this.M6 = value; return;
                    case 7: this.M7 = value; return;
                    case 8: this.M8 = value; return;
                    case 9: this.M9 = value; return;
                    case 10: this.MA = value; return;
                    case 11: this.MB = value; return;
                    case 12: this.MC = value; return;
                    case 13: this.MD = value; return;
                    case 14: this.ME = value; return;
                    case 15: this.MF = value; return;
                    default: throw new ArgumentOutOfRangeException(nameof(i), "Index must be between 0 and 15");
                }
            }
        }

        public Vector4f ColumnX4 {
            get => new Vector4f(this.M0, this.M4, this.M8, this.MC);
            set {
                this.M0 = value.x;
                this.M4 = value.y;
                this.M8 = value.z;
                this.MC = value.w;
            }
        }

        public Vector4f ColumnY4 {
            get => new Vector4f(this.M1, this.M5, this.M9, this.MD);
            set {
                this.M1 = value.x;
                this.M5 = value.y;
                this.M9 = value.z;
                this.MD = value.w;
            }
        }

        public Vector4f ColumnZ4 {
            get => new Vector4f(this.M2, this.M6, this.MA, this.ME);
            set {
                this.M2 = value.x;
                this.M6 = value.y;
                this.MA = value.z;
                this.ME = value.w;
            }
        }

        public Vector4f ColumnW4 {
            get => new Vector4f(this.M3, this.M7, this.MB, this.MF);
            set {
                this.M3 = value.x;
                this.M7 = value.y;
                this.MB = value.z;
                this.MF = value.w;
            }
        }

        public Vector3f ColumnX {
            get => new Vector3f(this.M0, this.M4, this.M8);
            set {
                this.M0 = value.x;
                this.M4 = value.y;
                this.M8 = value.z;
            }
        }

        public Vector3f ColumnY {
            get => new Vector3f(this.M1, this.M5, this.M9);
            set {
                this.M1 = value.x;
                this.M5 = value.y;
                this.M9 = value.z;
            }
        }

        public Vector3f ColumnZ {
            get => new Vector3f(this.M2, this.M6, this.MA);
            set {
                this.M2 = value.x;
                this.M6 = value.y;
                this.MA = value.z;
            }
        }

        public Vector3f ColumnW {
            get => new Vector3f(this.M3, this.M7, this.MB);
            set {
                this.M3 = value.x;
                this.M7 = value.y;
                this.MB = value.z;
            }
        }

        public Vector3f TranslationPart {
            get => new Vector3f(this.M3, this.M7, this.MB);
            set {
                this.M3 = value.x;
                this.M7 = value.y;
                this.MB = value.z;
            }
        }

        public Vector3f ScalePart {
            get => new Vector3f(this.M0, this.M5, this.MA);
            set {
                this.M0 = value.x;
                this.M5 = value.y;
                this.MA = value.z;
            }
        }

        public Matrix4(in Matrix4 matrix4) {
            this.M0 = matrix4.M0;
            this.M1 = matrix4.M1;
            this.M2 = matrix4.M2;
            this.M3 = matrix4.M3;
            this.M4 = matrix4.M4;
            this.M5 = matrix4.M5;
            this.M6 = matrix4.M6;
            this.M7 = matrix4.M7;
            this.M8 = matrix4.M8;
            this.M9 = matrix4.M9;
            this.MA = matrix4.MA;
            this.MB = matrix4.MB;
            this.MC = matrix4.MC;
            this.MD = matrix4.MD;
            this.ME = matrix4.ME;
            this.MF = matrix4.MF;
        }

        public void MakeIdentity() {
            this.M0 = 1.0f; this.M1 = 0.0f; this.M2 = 0.0f; this.M3 = 0.0f;
            this.M4 = 0.0f; this.M5 = 1.0f; this.M6 = 0.0f; this.M7 = 0.0f;
            this.M8 = 0.0f; this.M9 = 0.0f; this.MA = 1.0f; this.MB = 0.0f;
            this.MC = 0.0f; this.MD = 0.0f; this.ME = 0.0f; this.MF = 1.0f;
        }

        public void MakeRotationX(float r) {
            float cosR = (float) Math.Cos(r);
            float sinR = (float) Math.Sin(r);
            this.M0 = 1.0f; this.M1 = 0.0f; this.M2 = 0.0f;  this.M3 = 0.0f;
            this.M4 = 0.0f; this.M5 = cosR; this.M6 = -sinR; this.M7 = 0.0f;
            this.M8 = 0.0f; this.M9 = sinR; this.MA = cosR;  this.MB = 0.0f;
            this.MC = 0.0f; this.MD = 0.0f; this.ME = 0.0f;  this.MF = 1.0f;
        }

        public void MakeRotationY(float r) {
            float cosR = (float) Math.Cos(r);
            float sinR = (float) Math.Sin(r);
            this.M0 = cosR;  this.M1 = 0.0f; this.M2 = sinR; this.M3 = 0.0f;
            this.M4 = 0.0f;  this.M5 = 1.0f; this.M6 = 0.0f; this.M7 = 0.0f;
            this.M8 = -sinR; this.M9 = 0.0f; this.MA = cosR; this.MB = 0.0f;
            this.MC = 0.0f;  this.MD = 0.0f; this.ME = 0.0f; this.MF = 1.0f;
        }

        public void MakeRotationZ(float r) {
            float cosR = (float) Math.Cos(r);
            float sinR = (float) Math.Sin(r);
            this.M0 = cosR; this.M1 = -sinR; this.M2 = 0.0f; this.M3 = 0.0f;
            this.M4 = sinR; this.M5 = cosR;  this.M6 = 0.0f; this.M7 = 0.0f;
            this.M8 = 0.0f; this.M9 = 0.0f;  this.MA = 1.0f; this.MB = 0.0f;
            this.MC = 0.0f; this.MD = 0.0f;  this.ME = 0.0f; this.MF = 1.0f;
        }

        public void MakeTranslation(in Vector3f v) {
            this.M0 = 1.0f; this.M1 = 0.0f; this.M2 = 0.0f; this.M3 = v.x;
            this.M4 = 0.0f; this.M5 = 1.0f; this.M6 = 0.0f; this.M7 = v.y;
            this.M8 = 0.0f; this.M9 = 0.0f; this.MA = 1.0f; this.MB = v.z;
            this.MC = 0.0f; this.MD = 0.0f; this.ME = 0.0f; this.MF = 1.0f;
        }

        public void MakeTranslation(float x, float y, float z) {
            this.M0 = 1.0f; this.M1 = 0.0f; this.M2 = 0.0f; this.M3 = x;
            this.M4 = 0.0f; this.M5 = 1.0f; this.M6 = 0.0f; this.M7 = y;
            this.M8 = 0.0f; this.M9 = 0.0f; this.MA = 1.0f; this.MB = z;
            this.MC = 0.0f; this.MD = 0.0f; this.ME = 0.0f; this.MF = 1.0f;
        }

        public void MakeScale(in Vector3f v) {
            this.M0 = v.x;  this.M1 = 0.0f; this.M2 = 0.0f; this.M3 = 0.0f;
            this.M4 = 0.0f; this.M5 = v.y;  this.M6 = 0.0f; this.M7 = 0.0f;
            this.M8 = 0.0f; this.M9 = 0.0f; this.MA = v.z;  this.MB = 0.0f;
            this.MC = 0.0f; this.MD = 0.0f; this.ME = 0.0f; this.MF = 1.0f;
        }

        public void MakeScale(float x, float y, float z) {
            this.M0 = x;    this.M1 = 0.0f; this.M2 = 0.0f; this.M3 = 0.0f;
            this.M4 = 0.0f; this.M5 = y;    this.M6 = 0.0f; this.M7 = 0.0f;
            this.M8 = 0.0f; this.M9 = 0.0f; this.MA = z;    this.MB = 0.0f;
            this.MC = 0.0f; this.MD = 0.0f; this.ME = 0.0f; this.MF = 1.0f;
        }

        public Matrix4 Transpose() {
            Matrix4 n = new Matrix4();
            n.M0 = this.M0; n.M1 = this.M4; n.M2 = this.M8; n.M3 = this.MC;
            n.M4 = this.M1; n.M5 = this.M5; n.M6 = this.M9; n.M7 = this.MD;
            n.M8 = this.M2; n.M9 = this.M6; n.MA = this.MA; n.MB = this.ME;
            n.MC = this.M3; n.MD = this.M7; n.ME = this.MB; n.MF = this.MF;
            return n;
        }

        public static Matrix4 getInverse(in Matrix4 m) {
            Matrix4 inv = new Matrix4();
            inv.M0 = m.M5 * m.MA * m.MF -
                     m.M5 * m.MB * m.ME -
                     m.M9 * m.M6 * m.MF +
                     m.M9 * m.M7 * m.ME +
                     m.MD * m.M6 * m.MB -
                     m.MD * m.M7 * m.MA;

            inv.M4 = -m.M4 * m.MA * m.MF +
                     m.M4 * m.MB * m.ME +
                     m.M8 * m.M6 * m.MF -
                     m.M8 * m.M7 * m.ME -
                     m.MC * m.M6 * m.MB +
                     m.MC * m.M7 * m.MA;

            inv.M8 = m.M4 * m.M9 * m.MF -
                     m.M4 * m.MB * m.MD -
                     m.M8 * m.M5 * m.MF +
                     m.M8 * m.M7 * m.MD +
                     m.MC * m.M5 * m.MB -
                     m.MC * m.M7 * m.M9;

            inv.MC = -m.M4 * m.M9 * m.ME +
                      m.M4 * m.MA * m.MD +
                      m.M8 * m.M5 * m.ME -
                      m.M8 * m.M6 * m.MD -
                      m.MC * m.M5 * m.MA +
                      m.MC * m.M6 * m.M9;

            inv.M1 = -m.M1 * m.MA * m.MF +
                     m.M1 * m.MB * m.ME +
                     m.M9 * m.M2 * m.MF -
                     m.M9 * m.M3 * m.ME -
                     m.MD * m.M2 * m.MB +
                     m.MD * m.M3 * m.MA;

            inv.M5 = m.M0 * m.MA * m.MF -
                     m.M0 * m.MB * m.ME -
                     m.M8 * m.M2 * m.MF +
                     m.M8 * m.M3 * m.ME +
                     m.MC * m.M2 * m.MB -
                     m.MC * m.M3 * m.MA;

            inv.M9 = -m.M0 * m.M9 * m.MF +
                     m.M0 * m.MB * m.MD +
                     m.M8 * m.M1 * m.MF -
                     m.M8 * m.M3 * m.MD -
                     m.MC * m.M1 * m.MB +
                     m.MC * m.M3 * m.M9;

            inv.MD = m.M0 * m.M9 * m.ME -
                      m.M0 * m.MA * m.MD -
                      m.M8 * m.M1 * m.ME +
                      m.M8 * m.M2 * m.MD +
                      m.MC * m.M1 * m.MA -
                      m.MC * m.M2 * m.M9;

            inv.M2 = m.M1 * m.M6 * m.MF -
                     m.M1 * m.M7 * m.ME -
                     m.M5 * m.M2 * m.MF +
                     m.M5 * m.M3 * m.ME +
                     m.MD * m.M2 * m.M7 -
                     m.MD * m.M3 * m.M6;

            inv.M6 = -m.M0 * m.M6 * m.MF +
                     m.M0 * m.M7 * m.ME +
                     m.M4 * m.M2 * m.MF -
                     m.M4 * m.M3 * m.ME -
                     m.MC * m.M2 * m.M7 +
                     m.MC * m.M3 * m.M6;

            inv.MA = m.M0 * m.M5 * m.MF -
                      m.M0 * m.M7 * m.MD -
                      m.M4 * m.M1 * m.MF +
                      m.M4 * m.M3 * m.MD +
                      m.MC * m.M1 * m.M7 -
                      m.MC * m.M3 * m.M5;

            inv.ME = -m.M0 * m.M5 * m.ME +
                      m.M0 * m.M6 * m.MD +
                      m.M4 * m.M1 * m.ME -
                      m.M4 * m.M2 * m.MD -
                      m.MC * m.M1 * m.M6 +
                      m.MC * m.M2 * m.M5;

            inv.M3 = -m.M1 * m.M6 * m.MB +
                     m.M1 * m.M7 * m.MA +
                     m.M5 * m.M2 * m.MB -
                     m.M5 * m.M3 * m.MA -
                     m.M9 * m.M2 * m.M7 +
                     m.M9 * m.M3 * m.M6;

            inv.M7 = m.M0 * m.M6 * m.MB -
                     m.M0 * m.M7 * m.MA -
                     m.M4 * m.M2 * m.MB +
                     m.M4 * m.M3 * m.MA +
                     m.M8 * m.M2 * m.M7 -
                     m.M8 * m.M3 * m.M6;

            inv.MB = -m.M0 * m.M5 * m.MB +
                      m.M0 * m.M7 * m.M9 +
                      m.M4 * m.M1 * m.MB -
                      m.M4 * m.M3 * m.M9 -
                      m.M8 * m.M1 * m.M7 +
                      m.M8 * m.M3 * m.M5;

            inv.MF = m.M0 * m.M5 * m.MA -
                      m.M0 * m.M6 * m.M9 -
                      m.M4 * m.M1 * m.MA +
                      m.M4 * m.M2 * m.M9 +
                      m.M8 * m.M1 * m.M6 -
                      m.M8 * m.M2 * m.M5;

            float det = m[0] * inv[0] + m[1] * inv[4] + m[2] * inv[8] + m[3] * inv[12];
            return inv / det;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 Zero() {
            return new Matrix4();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 Identity() {
            Matrix4 matrix4 = new Matrix4();
            matrix4.MakeIdentity();
            return matrix4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 RotX(float r) {
            Matrix4 matrix4 = new Matrix4();
            matrix4.MakeRotationX(r);
            return matrix4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 RotY(float r) {
            Matrix4 matrix4 = new Matrix4();
            matrix4.MakeRotationY(r);
            return matrix4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 RotZ(float r) {
            Matrix4 matrix4 = new Matrix4();
            matrix4.MakeRotationZ(r);
            return matrix4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 Translation(in Vector3f v) {
            Matrix4 matrix4 = new Matrix4();
            matrix4.MakeTranslation(v);
            return matrix4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 Translation(float x, float y, float z) {
            Matrix4 matrix4 = new Matrix4();
            matrix4.MakeTranslation(x, y, z);
            return matrix4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 Scale(in Vector3f v) {
            Matrix4 matrix4 = new Matrix4();
            matrix4.MakeScale(v);
            return matrix4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 Scale(float x, float y, float z) {
            Matrix4 matrix4 = new Matrix4();
            matrix4.MakeScale(x, y, z);
            return matrix4;
        }

        public Vector3f MultiplyPoint(in Vector3f v) {
            float w = this.MC * v.x + this.MD * v.y + this.ME * v.z + this.MF;
            return new Vector3f(
                this.M0 * v.x + this.M1 * v.y + this.M2 * v.z + this.M3,
                this.M4 * v.x + this.M5 * v.y + this.M6 * v.z + this.M7,
                this.M8 * v.x + this.M9 * v.y + this.MA * v.z + this.MB) * w;
        }

        public Vector3f MultiplyDirection(in Vector3f v) {
            return new Vector3f(
                this.M0 * v.x + this.M1 * v.y + this.M2 * v.z,
                this.M4 * v.x + this.M5 * v.y + this.M6 * v.z,
                this.M8 * v.x + this.M9 * v.y + this.MA * v.z);
        }

        public static Vector4f operator *(in Matrix4 m, in Vector4f v) {
            return new Vector4f(
                m.M0  * v.x + m.M1  * v.y + m.M2  * v.z + m.M3  * v.w,
                m.M4  * v.x + m.M5  * v.y + m.M6  * v.z + m.M7  * v.w,
                m.M8  * v.x + m.M9  * v.y + m.MA * v.z + m.MB * v.w,
                m.MC * v.x + m.MD * v.y + m.ME * v.z + m.MF * v.w);
        }

        public static Vector4f operator *(in Matrix4 m, in Vector3f v) {
            return new Vector4f(
                m.M0  * v.x + m.M1  * v.y + m.M2  * v.z + m.M3,
                m.M4  * v.x + m.M5  * v.y + m.M6  * v.z + m.M7,
                m.M8  * v.x + m.M9  * v.y + m.MA * v.z + m.MB,
                m.MC * v.x + m.MD * v.y + m.ME * v.z + m.MF);
        }

        public static Matrix4 operator /(in Matrix4 m, float a) {
            Matrix4 n = new Matrix4();
            for (int i = 0; i < 16; i++) {
                n[i] = m[i] / a;
            }

            return n;
        }

        public static Matrix4 operator *(in Matrix4 m1, in Matrix4 m2) {
            return new Matrix4 {
                M0 = m2.M0 * m1.M0 + m2.M4 * m1.M1 + m2.M8 * m1.M2 + m2.MC * m1.M3,
                M1 = m2.M1 * m1.M0 + m2.M5 * m1.M1 + m2.M9 * m1.M2 + m2.MD * m1.M3,
                M2 = m2.M2 * m1.M0 + m2.M6 * m1.M1 + m2.MA * m1.M2 + m2.ME * m1.M3,
                M3 = m2.M3 * m1.M0 + m2.M7 * m1.M1 + m2.MB * m1.M2 + m2.MF * m1.M3,
                M4 = m2.M0 * m1.M4 + m2.M4 * m1.M5 + m2.M8 * m1.M6 + m2.MC * m1.M7,
                M5 = m2.M1 * m1.M4 + m2.M5 * m1.M5 + m2.M9 * m1.M6 + m2.MD * m1.M7,
                M6 = m2.M2 * m1.M4 + m2.M6 * m1.M5 + m2.MA * m1.M6 + m2.ME * m1.M7,
                M7 = m2.M3 * m1.M4 + m2.M7 * m1.M5 + m2.MB * m1.M6 + m2.MF * m1.M7,
                M8 = m2.M0 * m1.M8 + m2.M4 * m1.M9 + m2.M8 * m1.MA + m2.MC * m1.MB,
                M9 = m2.M1 * m1.M8 + m2.M5 * m1.M9 + m2.M9 * m1.MA + m2.MD * m1.MB,
                MA = m2.M2 * m1.M8 + m2.M6 * m1.M9 + m2.MA * m1.MA + m2.ME * m1.MB,
                MB = m2.M3 * m1.M8 + m2.M7 * m1.M9 + m2.MB * m1.MA + m2.MF * m1.MB,
                MC = m2.M0 * m1.MC + m2.M4 * m1.MD + m2.M8 * m1.ME + m2.MC * m1.MF,
                MD = m2.M1 * m1.MC + m2.M5 * m1.MD + m2.M9 * m1.ME + m2.MD * m1.MF,
                ME = m2.M2 * m1.MC + m2.M6 * m1.MD + m2.MA * m1.ME + m2.ME * m1.MF,
                MF = m2.M3 * m1.MC + m2.M7 * m1.MD + m2.MB * m1.ME + m2.MF * m1.MF
            };
        }

        public static Matrix4 Projection(float vpWidth, float vpHeight, float n, float f, float fov) {
            float fr = (float) (1.0f / Math.Tan(fov * Math.PI / 360.0f)); // fov in radians
            float a = vpHeight / vpWidth; // aspect ratio
            float d = n - f; // distance between near and far
            Matrix4 mat = new Matrix4();
            mat.M0 = fr * a; mat.M1 = 0.0f; mat.M2 = 0.0f;        mat.M3 = 0.0f;
            mat.M4 = 0.0f;   mat.M5 = fr;   mat.M6 = 0.0f;        mat.M7 = 0.0f;
            mat.M8 = 0.0f;   mat.M9 = 0.0f; mat.MA = (n + f) / d; mat.MB = (2 * n * f) / d;
            mat.MC = 0.0f;   mat.MD = 0.0f; mat.ME = -1.0f;       mat.MF = 0.0f;
            return mat;
        }

        /// <summary>
        /// Orthographic matrix
        /// </summary>
        /// <param name="l">Left</param>
        /// <param name="t">Top</param>
        /// <param name="r">Right</param>
        /// <param name="b">Bottom</param>
        /// <param name="n">Near</param>
        /// <param name="f">Far</param>
        /// <returns></returns>
        public static Matrix4 Orthographic(float l, float t, float r, float b, float n, float f) {
            Matrix4 mat = new Matrix4();
            mat.M0 = 2 / (r - l); mat.M1 = 0.0f;        mat.M2 = 0.0f;         mat.M3 = -(r + l) / (r - l);
            mat.M4 = 0.0f;        mat.M5 = 2 / (t - b); mat.M6 = 0.0f;         mat.M7 = -(t + b) / (t - b);
            mat.M8 = 0.0f;        mat.M9 = 0.0f;        mat.MA = -2 / (f - n); mat.MB = -(f + n) / (f - n);
            mat.MC = 0.0f;        mat.MD = 0.0f;        mat.ME = 0.0f;         mat.MF = 1.0f;
            return mat;
        }

        public static Matrix4 WorldToLocal(in Vector3f position, in Vector3f rotation, in Vector3f scale) {
            return Scale(1.0f / scale.x, 1.0f / scale.y, 1.0f / scale.z) *
                   RotZ(-rotation.z) *
                   RotX(-rotation.x) *
                   RotY(-rotation.y) *
                   Translation(-position.x, -position.y, -position.z);
        }

        public static Matrix4 LocalToWorld(in Vector3f position, in Vector3f rotation, in Vector3f scale) {
            return Translation(position) *
                   RotY(rotation.y) *
                   RotX(rotation.x) *
                   RotZ(rotation.z) *
                   Scale(scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4 Copy() {
            return new Matrix4(this);
        }
    }
}