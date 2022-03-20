using System;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace REghZy.Streams.Tests {
    internal class Program {
        static void Main(string[] args) {
            MemoryStream a = new MemoryStream(256);

            TestDataOutput(a, false);
            TestDataInput(a, false);

            MemoryStream b = new MemoryStream(256);
            TestDataOutput(b, true);
            TestDataInput(b, true);

            // BinaryWriter writer = new BinaryWriter(a);
            // writer.Write('d');
            // BinaryReader reader = new BinaryReader(a);
            // // why does this fail lol
            // reader.ReadChar();

            Matrix4x4 e;
        }

        public static void TestDataOutput(MemoryStream stream, bool littleEndianness) {
            IDataOutput output;
            // faster than having only the DataOutputStream support both little and big endianness
            if (littleEndianness) {
                output = new DataOutputStreamLE(stream);
            }
            else {
                output = new DataOutputStream(stream);
            }

            // enum
            output.WriteEnum08(TestEnum.A);
            output.WriteEnum16(TestEnum.B);
            output.WriteEnum32(TestEnum.C);
            output.WriteEnum64(TestEnum.D);

            // primitives explicit
            output.WriteSByte(1);
            output.WriteByte(2);
            output.WriteShort(3);
            output.WriteUShort(4);
            output.WriteInt(5);
            output.WriteUInt(6);
            output.WriteLong(7);
            output.WriteULong(8);
            output.WriteFloat(9.525f);
            output.WriteDouble(10.525f);
            output.WriteCharUTF16('h');
            output.WriteCharUTF8('i');
            output.WriteChar('t', Encoding.UTF32);
            output.WriteStringUTF16("aa");
            output.WriteStringUTF16("WriteStringUTF16");
            output.WriteStringUTF8("bb");
            output.WriteStringUTF8("WriteStringUTF8");
            output.WriteString("WriteStringUTF32", Encoding.UTF32);
            output.WriteCharsUTF16("cc".ToCharArray());
            output.WriteCharsUTF16("WriteCharsUTF16".ToCharArray());
            output.WriteCharsUTF8("dd".ToCharArray());
            output.WriteCharsUTF8("WriteCharsUTF8".ToCharArray());

            // primitives implicit
            output.WritePrimitive<sbyte>(1);
            output.WritePrimitive<byte>(2);
            output.WritePrimitive<short>(3);
            output.WritePrimitive<ushort>(4);
            output.WritePrimitive<int>(5);
            output.WritePrimitive<uint>(6);
            output.WritePrimitive<long>(7);
            output.WritePrimitive<ulong>(8);
            output.WritePrimitive<float>(9);
            output.WritePrimitive<double>(10);
            output.WritePrimitive(new TestStruct() {a = 15, b = 16, c = 17, d = 18});

            Console.Write("Breakpoint");
        }

        public static void TestDataInput(MemoryStream stream, bool littleEndianness) {
            IDataInput input;
            if (littleEndianness) {
                input = new DataInputStreamLE(stream, SeekOrigin.Begin);
            }
            else {
                input = new DataInputStream(stream, SeekOrigin.Begin);
            }

            // enum
            TestEnum a = input.ReadEnum08<TestEnum>();
            TestEnum b = input.ReadEnum16<TestEnum>();
            TestEnum c = input.ReadEnum32<TestEnum>();
            TestEnum d = input.ReadEnum64<TestEnum>();

            // primitives explicit
            sbyte   _1 = input.ReadSByte();
            byte    _2 = input.ReadByte();
            short   _3 = input.ReadShort();
            ushort  _4 = input.ReadUShort();
            int     _5 = input.ReadInt();
            uint    _6 = input.ReadUInt();
            long    _7 = input.ReadLong();
            ulong   _8 = input.ReadULong();
            float   _9 = input.ReadFloat();             // 9.525f
            double _10 = input.ReadDouble();            // 10.525f
            char     h = input.ReadCharUTF16();
            char     i = input.ReadCharUTF8();
            char     t = input.ReadChar(Encoding.UTF32);
            string  aa = input.ReadStringUTF16(2);
            string  a2 = input.ReadStringUTF16(16);     // "WriteStringUTF16"
            string  bb = input.ReadStringUTF8(2);
            string  b2 = input.ReadStringUTF8(15);      // "WriteStringUTF8"
            string  b3 = input.ReadString(Encoding.UTF32);
            char[]  cc = input.ReadCharsUTF16(2);
            char[]  c2 = input.ReadCharsUTF16(15);      // "WriteCharsUTF16"
            char[]  dd = input.ReadCharsUTF8(2);
            char[]  d2 = input.ReadCharsUTF8(14);       // WriteCharsUTF8

            // primitives implicit
            sbyte       p1 = input.ReadPrimitive<sbyte>();
            byte        p2 = input.ReadPrimitive<byte>();
            short       p3 = input.ReadPrimitive<short>();
            ushort      p4 = input.ReadPrimitive<ushort>();
            int         p5 = input.ReadPrimitive<int>();
            uint        p6 = input.ReadPrimitive<uint>();
            long        p7 = input.ReadPrimitive<long>();
            ulong       p8 = input.ReadPrimitive<ulong>();
            float       pf = input.ReadPrimitive<float>();     // 12.526f
            double      pd = input.ReadPrimitive<double>();    // 14.35321d
            TestStruct  ts = input.ReadPrimitive<TestStruct>(); // 15, 16, 17, 18

            Console.Write("Breakpoint");
        }
    }
}