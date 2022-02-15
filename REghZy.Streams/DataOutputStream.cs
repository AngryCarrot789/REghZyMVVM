using System;
using System.IO;
using System.Runtime.InteropServices;

namespace REghZy.Streams {
    /// <summary>
    /// A class for writing primitive objects to a stream
    /// <para>
    /// Most method have repeated code for speed reasons...
    /// </para>
    /// </summary>
    public class DataOutputStream : IDataOutput {
        private Stream stream;

        /// <summary>
        /// A temporary buffer used for writing
        /// </summary>
        private readonly byte[] buffer8 = new byte[8];

        public Stream Stream {
            get => this.stream;
            set => this.stream = value;
        }

        public DataOutputStream(Stream stream) {
            this.stream = stream;
        }

        public void Flush() {
            this.stream.Flush();
        }

        public void Close() {
            this.stream.Close();
        }

        public void Write(byte[] buf, int offset, int count) {
            this.stream.Write(buf, offset, count);
        }

        public void Write(byte[] buf, int offset = 0) {
            this.stream.Write(buf, offset, buf.Length);
        }

        public void WriteBoolean(bool val) {
            this.buffer8[0] = (byte) (val ? 1 : 0);
            this.stream.Write(this.buffer8, 0, 1);
        }

        public void WriteEnum8<TEnum>(TEnum value) where TEnum : unmanaged, Enum {
            unsafe {
                WriteByte(*((byte*) &value));
            }
        }

        public void WriteEnum16<TEnum>(TEnum value) where TEnum : unmanaged, Enum {
            unsafe {
                WriteUShort(*((ushort*) &value));
            }
        }

        public void WriteEnum32<TEnum>(TEnum value) where TEnum : unmanaged, Enum {
            unsafe {
                WriteUInt(*((uint*) &value));
            }
        }

        public void WriteEnum64<TEnum>(TEnum value) where TEnum : unmanaged, Enum {
            unsafe {
                WriteULong(*((ulong*) &value));
            }
        }

        public void WriteSByte(sbyte value) {
            this.buffer8[0] = (byte) (value & 255);
            this.stream.Write(this.buffer8, 0, 1);
        }

        public void WriteByte(byte value) {
            this.buffer8[0] = value;
            this.stream.Write(this.buffer8, 0, 1);
        }

        public void WriteShort(short value) {
            uint i = (uint) value;
            byte[] b = this.buffer8;
            b[0] = (byte) ((i >> 8) & 255);
            b[1] = (byte) ((i >> 0) & 255);
            this.stream.Write(b, 0, 2);
        }

        public void WriteUShort(ushort value) {
            uint i = value;
            byte[] b = this.buffer8;
            b[0] = (byte) ((i >> 8) & 255);
            b[1] = (byte) ((i >> 0) & 255);
            this.stream.Write(b, 0, 2);
        }

        public void WriteInt(int value) {
            uint i = (uint) value;
            byte[] b = this.buffer8;
            b[0] = (byte) ((i >> 24) & 255);
            b[1] = (byte) ((i >> 16) & 255);
            b[2] = (byte) ((i >> 8) & 255);
            b[3] = (byte) ((i >> 0) & 255);
            this.stream.Write(b, 0, 4);
        }

        public void WriteUInt(uint value) {
            byte[] b = this.buffer8;
            b[0] = (byte) ((value >> 24) & 255);
            b[1] = (byte) ((value >> 16) & 255);
            b[2] = (byte) ((value >> 8) & 255);
            b[3] = (byte) ((value >> 0) & 255);
            this.stream.Write(b, 0, 4);
        }

        public void WriteLong(long value) {
            ulong i = (ulong) value;
            byte[] b = this.buffer8;
            b[0] = (byte) ((i >> 56) & 255);
            b[1] = (byte) ((i >> 48) & 255);
            b[2] = (byte) ((i >> 40) & 255);
            b[3] = (byte) ((i >> 32) & 255);
            b[4] = (byte) ((i >> 24) & 255);
            b[5] = (byte) ((i >> 16) & 255);
            b[6] = (byte) ((i >> 8) & 255);
            b[7] = (byte) ((i >> 0) & 255);
            this.stream.Write(b, 0, 8);
        }

        public void WriteULong(ulong value) {
            byte[] b8 = this.buffer8;
            b8[0] = (byte) ((value >> 56) & 255);
            b8[1] = (byte) ((value >> 48) & 255);
            b8[2] = (byte) ((value >> 40) & 255);
            b8[3] = (byte) ((value >> 32) & 255);
            b8[4] = (byte) ((value >> 24) & 255);
            b8[5] = (byte) ((value >> 16) & 255);
            b8[6] = (byte) ((value >> 8) & 255);
            b8[7] = (byte) ((value >> 0) & 255);
            this.stream.Write(b8, 0, 8);

            // TODO: use Unsafe.As, because it's IL optimised to do casting
            // But it does mean this would become low-endianness...
            // this.stream.Write(Unsafe.As<ulong, byte[]>(ref value), 0, 8);
        }

        public void WriteFloat(float value) {
            unsafe {
                uint bits = *(uint*) &value;
                byte[] b = this.buffer8;
                b[0] = (byte) ((bits >> 24) & 255);
                b[1] = (byte) ((bits >> 16) & 255);
                b[2] = (byte) ((bits >> 8) & 255);
                b[3] = (byte) ((bits >> 0) & 255);
                this.stream.Write(b, 0, 4);
            }
        }

        public void WriteDouble(double value) {
            unsafe {
                ulong bits = *(ulong*) &value;
                byte[] b = this.buffer8;
                b[0] = (byte) ((bits >> 56) & 255);
                b[1] = (byte) ((bits >> 48) & 255);
                b[2] = (byte) ((bits >> 40) & 255);
                b[3] = (byte) ((bits >> 32) & 255);
                b[4] = (byte) ((bits >> 24) & 255);
                b[5] = (byte) ((bits >> 16) & 255);
                b[6] = (byte) ((bits >> 8) & 255);
                b[7] = (byte) ((bits >> 0) & 255);
                this.stream.Write(b, 0, 8);
            }
        }

        public void WriteCharUTF16(char value) {
            byte[] b = this.buffer8;
            b[0] = (byte) ((value >> 8) & 255);
            b[1] = (byte) ((value >> 0) & 255);
            this.stream.Write(b, 0, 2);
        }

        public void WriteCharUTF8(char value) {
            this.buffer8[0] = (byte) (value & 255);
            this.stream.Write(this.buffer8, 0, 1);
        }

        public void WriteStringUTF16(string value) {
            int len = value.Length;
            if (len > 3) {
                unsafe {
                    fixed (char* ptr = value) {
                        WritePtrUTF16(ptr, 0, len);
                    }
                }
            }
            else {
                byte[] b = this.buffer8;
                if (len == 3) {
                    char c1 = value[0];
                    char c2 = value[1];
                    char c3 = value[2];
                    b[0] = (byte) ((c1 >> 8) & 255);
                    b[1] = (byte) ((c1 >> 0) & 255);
                    b[2] = (byte) ((c2 >> 8) & 255);
                    b[3] = (byte) ((c2 >> 0) & 255);
                    b[4] = (byte) ((c3 >> 8) & 255);
                    b[5] = (byte) ((c3 >> 0) & 255);
                    this.stream.Write(b, 0, 6);
                }
                else if (len == 2) {
                    char c1 = value[0];
                    char c2 = value[1];
                    b[0] = (byte) ((c1 >> 8) & 255);
                    b[1] = (byte) ((c1 >> 0) & 255);
                    b[2] = (byte) ((c2 >> 8) & 255);
                    b[3] = (byte) ((c2 >> 0) & 255);
                    this.stream.Write(b, 0, 4);
                }
                else if (len == 1) {
                    char c = value[0];
                    b[0] = (byte) ((c >> 8) & 255);
                    b[1] = (byte) ((c >> 0) & 255);
                    this.stream.Write(b, 0, 2);
                }
            }
        }

        public void WriteStringUTF8(string value) {
            int len = value.Length;
            if (len > 3) {
                unsafe {
                    fixed (char* ptr = value) {
                        WritePtrUTF8(ptr, 0, len);
                    }
                }
            }
            else {
                byte[] b = this.buffer8;
                if (len == 3) {
                    b[0] = (byte) (value[0] & 255);
                    b[1] = (byte) (value[1] & 255);
                    b[2] = (byte) (value[2] & 255);
                    this.stream.Write(b, 0, 3);
                }
                else if (len == 2) {
                    b[0] = (byte) (value[0] & 255);
                    b[1] = (byte) (value[1] & 255);
                    this.stream.Write(b, 0, 2);
                }
                else if (len == 1) {
                    b[0] = (byte) (value[0] & 255);
                    this.stream.Write(b, 0, 1);
                }
            }
        }

        public void WriteCharsUTF16(char[] chars) {
            int len = chars.Length;
            if (len > 3) {
                unsafe {
                    fixed (char* ptr = chars) {
                        WritePtrUTF16(ptr, 0, len);
                    }
                }
            }
            else {
                byte[] b = this.buffer8;
                if (len == 3) {
                    char c1 = chars[0];
                    char c2 = chars[1];
                    char c3 = chars[2];
                    b[0] = (byte) ((c1 >> 8) & 255);
                    b[1] = (byte) ((c1 >> 0) & 255);
                    b[2] = (byte) ((c2 >> 8) & 255);
                    b[3] = (byte) ((c2 >> 0) & 255);
                    b[4] = (byte) ((c3 >> 8) & 255);
                    b[5] = (byte) ((c3 >> 0) & 255);
                    this.stream.Write(b, 0, 6);
                }
                else if (len == 2) {
                    char c1 = chars[0];
                    char c2 = chars[1];
                    b[0] = (byte) ((c1 >> 8) & 255);
                    b[1] = (byte) ((c1 >> 0) & 255);
                    b[2] = (byte) ((c2 >> 8) & 255);
                    b[3] = (byte) ((c2 >> 0) & 255);
                    this.stream.Write(b, 0, 4);
                }
                else if (len == 1) {
                    char c = chars[0];
                    b[0] = (byte) ((c >> 8) & 255);
                    b[1] = (byte) ((c >> 0) & 255);
                    this.stream.Write(b, 0, 2);
                }
            }
        }

        public void WriteCharsUTF8(char[] chars) {
            int len = chars.Length;
            if (len > 3) {
                unsafe {
                    fixed (char* ptr = chars) {
                        WritePtrUTF8(ptr, 0, len);
                    }
                }
            }
            else {
                byte[] b = this.buffer8;
                if (len == 3) {
                    b[0] = (byte) (chars[0] & 255);
                    b[1] = (byte) (chars[1] & 255);
                    b[2] = (byte) (chars[2] & 255);
                    this.stream.Write(b, 0, 3);
                }
                else if (len == 2) {
                    b[0] = (byte) (chars[0] & 255);
                    b[1] = (byte) (chars[1] & 255);
                    this.stream.Write(b, 0, 2);
                }
                else if (len == 1) {
                    b[0] = (byte) (chars[0] & 255);
                    this.stream.Write(b, 0, 1);
                }
            }
        }

        public unsafe void WritePtrUTF16(char* ptr, int offset, int length) {
            byte[] b = this.buffer8;
            Stream s = this.stream;
            fixed (byte* bptr = this.buffer8) {
                while (length > 3) {
                    bptr[0] = (byte) ((ptr[offset + 0] >> 8) & 255);
                    bptr[1] = (byte) ((ptr[offset + 0] >> 0) & 255);
                    bptr[2] = (byte) ((ptr[offset + 1] >> 8) & 255);
                    bptr[3] = (byte) ((ptr[offset + 1] >> 0) & 255);
                    bptr[4] = (byte) ((ptr[offset + 2] >> 8) & 255);
                    bptr[5] = (byte) ((ptr[offset + 2] >> 0) & 255);
                    bptr[6] = (byte) ((ptr[offset + 3] >> 8) & 255);
                    bptr[7] = (byte) ((ptr[offset + 3] >> 0) & 255);
                    offset += 4;
                    length -= 4;
                    s.Write(b, 0, 8);
                }

                if (length == 3) {
                    bptr[0] = (byte) ((ptr[offset + 0] >> 8) & 255);
                    bptr[1] = (byte) ((ptr[offset + 0] >> 0) & 255);
                    bptr[2] = (byte) ((ptr[offset + 1] >> 8) & 255);
                    bptr[3] = (byte) ((ptr[offset + 1] >> 0) & 255);
                    bptr[4] = (byte) ((ptr[offset + 2] >> 8) & 255);
                    bptr[5] = (byte) ((ptr[offset + 2] >> 0) & 255);
                    s.Write(b, 0, 6);
                }
                else if (length == 2) {
                    bptr[0] = (byte) ((ptr[offset + 0] >> 8) & 255);
                    bptr[1] = (byte) ((ptr[offset + 0] >> 0) & 255);
                    bptr[2] = (byte) ((ptr[offset + 1] >> 8) & 255);
                    bptr[3] = (byte) ((ptr[offset + 1] >> 0) & 255);
                    s.Write(b, 0, 4);
                }
                else if (length == 1) {
                    bptr[0] = (byte) ((ptr[offset] >> 8) & 255);
                    bptr[1] = (byte) ((ptr[offset] >> 0) & 255);
                    s.Write(b, 0, 2);
                }
            }
        }

        public unsafe void WritePtrUTF8(char* cptr, int offset, int length) {
            byte[] b = this.buffer8;
            Stream s = this.stream;
            fixed (byte* bptr = b) {
                while (length > 7) {
                    bptr[0] = (byte) (cptr[offset + 0] & 255);
                    bptr[1] = (byte) (cptr[offset + 1] & 255);
                    bptr[2] = (byte) (cptr[offset + 2] & 255);
                    bptr[3] = (byte) (cptr[offset + 3] & 255);
                    bptr[4] = (byte) (cptr[offset + 4] & 255);
                    bptr[5] = (byte) (cptr[offset + 5] & 255);
                    bptr[6] = (byte) (cptr[offset + 6] & 255);
                    bptr[7] = (byte) (cptr[offset + 7] & 255);
                    offset += 8;
                    length -= 8;
                    s.Write(b, 0, 8);
                }

                if (length > 3) {
                    bptr[0] = (byte) (cptr[offset + 0] & 255);
                    bptr[1] = (byte) (cptr[offset + 1] & 255);
                    bptr[2] = (byte) (cptr[offset + 2] & 255);
                    bptr[3] = (byte) (cptr[offset + 3] & 255);
                    offset += 4;
                    length -= 4;
                    s.Write(b, 0, 4);
                }

                if (length == 3) {
                    bptr[0] = (byte) (cptr[offset + 0] & 255);
                    bptr[1] = (byte) (cptr[offset + 1] & 255);
                    bptr[2] = (byte) (cptr[offset + 2] & 255);
                    s.Write(b, 0, 3);
                }
                else if (length == 2) {
                    bptr[0] = (byte) (cptr[offset + 0] & 255);
                    bptr[1] = (byte) (cptr[offset + 1] & 255);
                    s.Write(b, 0, 2);
                }
                else if (length == 1) {
                    bptr[0] = (byte) (cptr[offset] & 255);
                    s.Write(b, 0, 1);
                }
            }
        }

        public void WritePtr(IntPtr ptr, int offset, int length) {
            byte[] b = this.buffer8;
            Stream s = this.stream;
            while (true) {
                if (length > 7) {
                    long i8 = Marshal.ReadInt64(ptr, offset);
                    b[0] = (byte) ((i8 >> 56) & 255);
                    b[1] = (byte) ((i8 >> 48) & 255);
                    b[2] = (byte) ((i8 >> 40) & 255);
                    b[3] = (byte) ((i8 >> 32) & 255);
                    b[4] = (byte) ((i8 >> 24) & 255);
                    b[5] = (byte) ((i8 >> 16) & 255);
                    b[6] = (byte) ((i8 >> 8) & 255);
                    b[7] = (byte) ((i8 >> 0) & 255);
                    offset += 8;
                    length -= 8;
                    s.Write(b, 0, 8);
                    continue;
                }
                else if (length > 3) {
                    int i4 = Marshal.ReadInt32(ptr);
                    b[0] = (byte) ((i4 >> 24) & 255);
                    b[1] = (byte) ((i4 >> 16) & 255);
                    b[2] = (byte) ((i4 >> 8) & 255);
                    b[3] = (byte) ((i4 >> 0) & 255);
                    offset += 4;
                    length -= 4;
                    s.Write(b, 0, 4);
                    continue;
                }
                else if (length == 3) {
                    b[0] = Marshal.ReadByte(ptr, offset);
                    b[1] = Marshal.ReadByte(ptr, offset + 1);
                    b[2] = Marshal.ReadByte(ptr, offset + 2);
                    s.Write(b, 0, 3);
                    return;
                }
                else if (length == 2) {
                    b[0] = Marshal.ReadByte(ptr, offset);
                    b[1] = Marshal.ReadByte(ptr, offset + 1);
                    s.Write(b, 0, 2);
                    return;
                }
                else if (length == 1) {
                    b[0] = Marshal.ReadByte(ptr, offset);
                    s.Write(b, 0, 1);
                    return;
                }
                else {
                    return;
                }
            }
        }

        public unsafe void WritePtr(byte* ptr, int offset, int length) {
            byte[] b = this.buffer8;
            Stream s = this.stream;
            while (true) {
                if (length > 7) {
                    b[0] = ptr[offset + 0];
                    b[1] = ptr[offset + 1];
                    b[2] = ptr[offset + 2];
                    b[3] = ptr[offset + 3];
                    b[4] = ptr[offset + 4];
                    b[5] = ptr[offset + 5];
                    b[6] = ptr[offset + 6];
                    b[7] = ptr[offset + 7];
                    offset += 8;
                    length -= 8;
                    s.Write(b, 0, 8);
                    continue;
                }
                else if (length > 3) {
                    b[0] = ptr[offset + 0];
                    b[1] = ptr[offset + 1];
                    b[2] = ptr[offset + 2];
                    b[3] = ptr[offset + 3];
                    offset += 4;
                    length -= 4;
                    s.Write(b, 0, 4);
                    continue;
                }
                else if (length == 3) {
                    b[0] = ptr[offset + 0];
                    b[1] = ptr[offset + 1];
                    b[2] = ptr[offset + 2];
                    s.Write(b, 0, 3);
                    return;
                }
                else if (length == 2) {
                    b[0] = ptr[offset + 0];
                    b[1] = ptr[offset + 1];
                    s.Write(b, 0, 2);
                    return;
                }
                else if (length == 1) {
                    b[0] = ptr[offset + 0];
                    s.Write(b, 0, 1);
                    return;
                }
                else {
                    return;
                }
            }
        }

        public void WritePrimitive<T>(T value) where T : unmanaged {
            unsafe {
                WritePtr((byte*) &value, 0, sizeof(T));
            }
        }

        public void WritePrimitiveLabelled<T>(T value, int length) where T : unmanaged {
            WriteUShort((ushort) length);
            unsafe {
                WritePtr((byte*) &value, 0, length);
            }
        }

        public void WritePrimitiveLabelled<T>(T value) where T : unmanaged {
            unsafe {
                ushort length = (ushort) sizeof(T);
                WriteUShort(length);
                WritePtr((byte*) &value, 0, length);
            }
        }
    }
}