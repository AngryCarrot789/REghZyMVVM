using System;
using System.IO;
using REghZy.Streams;

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
            // new Program();

            MemoryStream stream = new MemoryStream(128);

            // 0b000011111111000000001111111110101010
            // 4,278,255,530
            // 0xFF00FFAA
            // output.WriteUInt(0xFF00FFAA);
            // output.WritePrimitive<uint>(0xFF00FFAA);

            DataOutputStreamLE output = new DataOutputStreamLE(stream);
            output.WriteStringUTF8("hello lol");

            DataInputStreamLE input = new DataInputStreamLE(stream, SeekOrigin.Begin);
            string okay = input.ReadStringUTF8(9);

            Console.Write("e");

            // output.WritePrimitive<int>(45);
            // output.WritePrimitive<byte>(20);
            // KP_DATA data = new KP_DATA() { a = 5, b = 10, c = 15, d = 20 };
            // BlitUtils.LoadString(data.arr, "hello");
            // output.WritePrimitive(data);

            // byte[] qq;
            // fixed (byte* p = (qq = new byte[8])) {
            //     input.ReadPtr(p, 0, 8);
            // }

            // int a = input.ReadPrimitive<int>();
            // byte b = input.ReadPrimitive<byte>();
            // KP_DATA c = input.ReadPrimitive<KP_DATA>();
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