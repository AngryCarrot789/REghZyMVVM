using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using REghzy.MathsF;
using REghZy.MathsF;
using REghZy.Streams;
using REghZy.Utils;

namespace REghZy.MVVM.Test {
    internal class Program {
        public static byte[] All(int len) {
            byte[] arr = new byte[len];
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = (byte) (i & 255);
            }

            return arr;
        }

        public static unsafe void Main(string[] args) {
            Quaternion a = new Quaternion(0.2f, 0.5f, 0.6f, 0.9f);
            Quaternion b = new Quaternion(0.4f, 0.2f, 0.3f, 0.1f);
            Vector3f c = a.RotateVector0(new Vector3f(0.5f, 0.9f, 1.3f));
            Vector3f d = a.RotateVector1(new Vector3f(0.5f, 0.9f, 1.3f));

            Matrix4 m = new Matrix4();


            Vector3f vec = new Vector3f(5.0f, 5.0f, 5.0f) * Vector3f.UnitX;




            new Program();

            MemoryStream stream = new MemoryStream(128);
            DataOutputStream output = new DataOutputStream(stream);

            // 0b000011111111000000001111111110101010
            // 4,278,255,530
            // 0xFF00FFAA
            // output.WriteUInt(0xFF00FFAA);
            // output.WritePrimitive<uint>(0xFF00FFAA);

            fixed (char* ptr = "hello") {
                output.WriteStringUTF16("hi");
                output.WritePtrUTF16(ptr, 0, 5);
            }


            // output.WritePrimitive<int>(45);
            // output.WritePrimitive<byte>(20);
            // KP_DATA data = new KP_DATA() { a = 5, b = 10, c = 15, d = 20 };
            // BlitUtils.LoadString(data.arr, "hello");
            // output.WritePrimitive(data);

            DataInputStream input = new DataInputStream(stream, SeekOrigin.Begin);

            string h = input.ReadStringUTF8(5);

            // byte[] qq;
            // fixed (byte* p = (qq = new byte[8])) {
            //     input.ReadPtr(p, 0, 8);
            // }

            char[] chars = input.ReadCharsUTF8(input.ReadShort());

            // int a = input.ReadPrimitive<int>();
            // byte b = input.ReadPrimitive<byte>();
            // KP_DATA c = input.ReadPrimitive<KP_DATA>();

            Console.Write("e");
        }

        public unsafe struct KP_DATA {
            public int a;
            public int b;
            public short c;
            public short d;
            public fixed char arr[5];
        }
    }
}