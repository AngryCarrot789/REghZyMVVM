using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
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
            // int[] a = ArrayUtils.RemoveAt(new int[] { 1, 2, 3, 4, 5 }, 4);

            MemoryStream stream = new MemoryStream(128);
            DataOutputStream output = new DataOutputStream(stream);

            // char[] array = new char[5];
            // fixed (char* ptr = array) {
            //     BlitUtils.LoadString(ptr, "hello");
            // }

            fixed (byte* ptr = All(8)) {
                output.WritePtr(ptr, 0, 8);
            }

            output.WriteShort(15);
            output.WriteStringUTF8("hello there lol");

            // output.WritePrimitive<int>(45);
            // output.WritePrimitive<byte>(20);
            // KP_DATA data = new KP_DATA() { a = 5, b = 10, c = 15, d = 20 };
            // BlitUtils.LoadString(data.arr, "hello");
            // output.WritePrimitive(data);

            DataInputStream input = new DataInputStream(stream, SeekOrigin.Begin);

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