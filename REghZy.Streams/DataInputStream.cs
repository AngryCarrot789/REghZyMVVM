using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace REghZy.Streams {
    /// <summary>
    /// A class for reading primitive objects from a stream
    /// <para>
    /// Most method have repeated code for speed reasons...
    /// </para>
    /// </summary>
    public class DataInputStream : IDataInput {
        private Stream stream;

        /// <summary>
        /// A small buffer for reading into
        /// </summary>
        private readonly byte[] buffer8 = new byte[8];

        // nullable, for speed reasons. care must be taken to ensure the steam isn't null while in use
        public Stream Stream {
            get => this.stream;
            set => this.stream = value;
        }

        public DataInputStream(Stream stream) {
            this.stream = stream;
        }

        public DataInputStream(Stream stream, SeekOrigin origin, long offset = 0) {
            this.stream = stream;
            stream.Seek(offset, origin);
        }

        public void Close() {
            this.stream.Close();
        }

        public int Read(byte[] dest, int offset, int count) {
            return this.stream.Read(dest, offset, count);
        }

        public void ReadFully(byte[] dest) {
            ReadFully(dest, 0, dest.Length);
        }

        public void ReadFully(byte[] dest, int offset, int length) {
            int n = 0;
            Stream s = this.stream;
            while (n < length) {
                n += s.Read(dest, offset + n, length - n);
            }
        }

        public bool ReadBool() {
            if (this.stream.Read(this.buffer8, 0, 1) != 1) {
                throw new EndOfStreamException("Failed to read 1 byte for a boolean");
            }

            return this.buffer8[0] == 1;
        }

        public T ReadEnum8<T>() where T : unmanaged, Enum {
            byte value = ReadByte();
            unsafe {
                return *(T*) &value;
            }
        }

        public T ReadEnum16<T>() where T : unmanaged, Enum {
            ushort value = ReadUShort();
            unsafe {
                return *(T*) &value;
            }
        }

        public T ReadEnum32<T>() where T : unmanaged, Enum {
            uint value = ReadUInt();
            unsafe {
                return *(T*) &value;
            }
        }

        public T ReadEnum64<T>() where T : unmanaged, Enum {
            ulong value = ReadULong();
            unsafe {
                return *(T*) &value;
            }
        }

        public sbyte ReadSByte() {
            int read = this.stream.Read(this.buffer8, 0, 1);
            if (read != 1) {
                throw new EndOfStreamException("Failed to read 1 byte for an sbyte");
            }

            return (sbyte) this.buffer8[0];
        }

        public byte ReadByte() {
            int read = this.stream.Read(this.buffer8, 0, 1);
            if (read != 1) {
                throw new EndOfStreamException("Failed to read 1 byte for a byte");
            }

            return this.buffer8[0];
        }

        public short ReadShort() {
            byte[] b = this.buffer8;
            if (this.stream.Read(b, 0, 2) != 2) {
                throw new EndOfStreamException("Failed to read 2 bytes for a short");
            }

            return (short) ((b[0] << 8) + (b[1] << 0));
        }

        public ushort ReadUShort() {
            byte[] b = this.buffer8;
            if (this.stream.Read(b, 0, 2) != 2) {
                throw new EndOfStreamException("Failed to read 2 bytes for a ushort");
            }

            return (ushort) ((b[0] << 8) + (b[1] << 0));
        }

        public int ReadInt() {
            byte[] b = this.buffer8;
            if (this.stream.Read(b, 0, 4) != 4) {
                throw new EndOfStreamException("Failed to read 4 bytes for a int");
            }

            return (int) (((uint) b[0] << 24) +
                          ((uint) b[1] << 16) +
                          ((uint) b[2] << 8) +
                          ((uint) b[3] << 0));
        }

        public uint ReadUInt() {
            byte[] b = this.buffer8;
            if (this.stream.Read(b, 0, 4) != 4) {
                throw new EndOfStreamException("Failed to read 4 bytes for a uint");
            }

            this.buffer8[9] = 6;

            return ((uint) b[0] << 24) +
                   ((uint) b[1] << 16) +
                   ((uint) b[2] << 8) +
                   ((uint) b[3] << 0);
        }

        public long ReadLong() {
            byte[] bArr8 = this.buffer8;
            if (this.stream.Read(bArr8, 0, 8) != 8) {
                throw new EndOfStreamException("Failed to read 8 bytes for a long");
            }

            return (long) (((ulong) bArr8[0] << 56) +
                           ((ulong) bArr8[1] << 48) +
                           ((ulong) bArr8[2] << 40) +
                           ((ulong) bArr8[3] << 32) +
                           ((ulong) bArr8[4] << 24) +
                           ((ulong) bArr8[5] << 16) +
                           ((ulong) bArr8[6] << 8) +
                           ((ulong) bArr8[7] << 0));
        }

        public ulong ReadULong() {
            byte[] b = this.buffer8;
            if (this.stream.Read(b, 0, 8) != 8) {
                throw new EndOfStreamException("Failed to read 8 bytes for a ulong");
            }

            return ((ulong) b[0] << 56) +
                   ((ulong) b[1] << 48) +
                   ((ulong) b[2] << 40) +
                   ((ulong) b[3] << 32) +
                   ((ulong) b[4] << 24) +
                   ((ulong) b[5] << 16) +
                   ((ulong) b[6] << 8) +
                   ((ulong) b[7] << 0);
        }

        public float ReadFloat() {
            byte[] b = this.buffer8;
            if (this.stream.Read(b, 0, 4) != 4) {
                throw new EndOfStreamException("Failed to read 4 bytes for a float");
            }

            unsafe {
                uint p0 = ((uint) b[0] << 24) +
                          ((uint) b[1] << 16) +
                          ((uint) b[2] << 8) +
                          ((uint) b[3] << 0);
                return *(float*) &p0;
            }
        }

        public double ReadDouble() {
            byte[] b = this.buffer8;
            if (this.stream.Read(b, 0, 8) != 8) {
                throw new EndOfStreamException("Failed to read 8 bytes for a double");
            }

            unsafe {
                ulong p0 = ((ulong) b[0] << 56) +
                           ((ulong) b[1] << 48) +
                           ((ulong) b[2] << 40) +
                           ((ulong) b[3] << 32) +
                           ((ulong) b[4] << 24) +
                           ((ulong) b[5] << 16) +
                           ((ulong) b[6] << 8) +
                           ((ulong) b[7] << 0);
                return *(double*) &p0;
            }
        }

        public char ReadCharUTF16() {
            byte[] b = this.buffer8;
            if (this.stream.Read(b, 0, 2) != 2) {
                throw new EndOfStreamException("Failed to read 2 bytes for a char");
            }

            return (char) (ushort) ((b[0] << 8) + (b[1] << 0));
        }

        public char ReadCharUTF8() {
            if (this.stream.Read(this.buffer8, 0, 1) != 1) {
                throw new EndOfStreamException("Failed to read 1 byte for a char");
            }

            return (char) this.buffer8[0];
        }

        public string ReadStringUTF16(int len) {
            return new string(ReadCharsUTF16(len));
        }

        public string ReadStringUTF8(int len) {
            return new string(ReadCharsUTF8(len));
        }

        public char[] ReadCharsUTF16(int length) {
            char[] chars = new char[length];
            if (length == 0) {
                return chars;
            }

            unsafe {
                // ptr for unchecked indexing (which should be faster)
                byte[] b = this.buffer8;
                Stream s = this.stream;
                if (length > 3) {
                    int i = 0;
                    fixed (char* cptr = chars) {
                        while (length > 3) {
                            if (s.Read(b, 0, 8) != 8) {
                                throw new EndOfStreamException($"Failed to read 8 bytes for 4 chars ({length} bytes remaining, read {i} so far)");
                            }

                            cptr[i + 0] = (char) (ushort) ((b[0] << 8) + (b[1] << 0));
                            cptr[i + 1] = (char) (ushort) ((b[2] << 8) + (b[3] << 0));
                            cptr[i + 2] = (char) (ushort) ((b[4] << 8) + (b[5] << 0));
                            cptr[i + 3] = (char) (ushort) ((b[6] << 8) + (b[7] << 0));
                            length -= 4;
                            i += 4;
                        }

                        if (length == 3) {
                            if (s.Read(b, 0, 6) != 6) {
                                throw new EndOfStreamException($"Failed to read 6 bytes for 3 chars ({length} bytes remaining, last 3 chars, read {i} so far)");
                            }

                            cptr[i + 0] = (char) (ushort) ((b[0] << 8) + (b[1] << 0));
                            cptr[i + 1] = (char) (ushort) ((b[2] << 8) + (b[3] << 0));
                            cptr[i + 2] = (char) (ushort) ((b[4] << 8) + (b[5] << 0));
                        }
                        else if (length == 2) {
                            if (s.Read(b, 0, 4) != 4) {
                                throw new EndOfStreamException($"Failed to read 4 bytes for 2 chars ({length} bytes remaining, last 2 chars, read {i} so far)");
                            }

                            cptr[i + 0] = (char) (ushort) ((b[0] << 8) + (b[1] << 0));
                            cptr[i + 1] = (char) (ushort) ((b[2] << 8) + (b[3] << 0));
                        }
                        else if (length == 1) {
                            if (s.Read(b, 0, 2) != 2) {
                                throw new EndOfStreamException($"Failed to read 2 bytes for 1 char ({length} bytes remaining, last 1 char, read {i} so far)");
                            }

                            cptr[i] = (char) (ushort) ((b[0] << 8) + (b[1] << 0));
                        }

                        return chars;
                    }
                }
                else if (length == 3) {
                    if (s.Read(b, 0, 6) != 6) {
                        throw new EndOfStreamException("Failed to read 6 bytes for 3 chars (in string len 3)");
                    }

                    chars[0] = (char) (ushort) ((b[0] << 8) + (b[1] << 0));
                    chars[1] = (char) (ushort) ((b[2] << 8) + (b[3] << 0));
                    chars[2] = (char) (ushort) ((b[4] << 8) + (b[5] << 0));
                    return chars;
                }
                else if (length == 2) {
                    if (s.Read(b, 0, 4) != 4) {
                        throw new EndOfStreamException("Failed to read 4 bytes for 2 chars (in string len 2)");
                    }

                    chars[0] = (char) (ushort) ((b[0] << 8) + (b[1] << 0));
                    chars[1] = (char) (ushort) ((b[2] << 8) + (b[3] << 0));
                    return chars;
                }
                else if (length == 1) {
                    if (s.Read(b, 0, 2) != 2) {
                        throw new EndOfStreamException("Failed to read 2 bytes for a char (in string len 1)");
                    }

                    chars[0] = (char) (ushort) ((b[0] << 8) + (b[1] << 0));
                    return chars;
                }
                else {
                    return chars;
                }
            }
        }

        public char[] ReadCharsUTF8(int length) {
            char[] chars = new char[length];
            if (length == 0) {
                return chars;
            }

            unsafe {
                Stream s = this.stream;
                byte[] b = this.buffer8;
                if (length > 3) {
                    int i = 0;
                    fixed (char* cptr = chars) {
                        while (length > 7) {
                            if (s.Read(b, 0, 8) != 8) {
                                throw new EndOfStreamException($"Failed to read 8 bytes for 8 chars ({length} bytes remaining, read {i} so far)");
                            }

                            cptr[i + 0] = (char) b[0];
                            cptr[i + 1] = (char) b[1];
                            cptr[i + 2] = (char) b[2];
                            cptr[i + 3] = (char) b[3];
                            cptr[i + 4] = (char) b[4];
                            cptr[i + 5] = (char) b[5];
                            cptr[i + 6] = (char) b[6];
                            cptr[i + 7] = (char) b[7];
                            length -= 8;
                            i += 8;
                        }

                        if (length > 3) {
                            if (s.Read(b, 0, 4) != 4) {
                                throw new EndOfStreamException($"Failed to read 4 bytes for 4 chars ({length} bytes remaining, read {i} so far)");
                            }

                            cptr[i + 0] = (char) b[0];
                            cptr[i + 1] = (char) b[1];
                            cptr[i + 2] = (char) b[2];
                            cptr[i + 3] = (char) b[3];
                            length -= 4;
                            i += 4;
                        }

                        if (length == 3) {
                            if (s.Read(b, 0, 3) != 3) {
                                throw new EndOfStreamException($"Failed to read 3 bytes for 3 chars ({length} bytes remaining, last 3 chars, read {i} so far)");
                            }

                            cptr[i + 0] = (char) b[0];
                            cptr[i + 1] = (char) b[1];
                            cptr[i + 2] = (char) b[2];
                            return chars;
                        }
                        else if (length == 2) {
                            if (s.Read(b, 0, 2) != 2) {
                                throw new EndOfStreamException($"Failed to read 2 bytes for 2 chars ({length} bytes remaining, last 2 chars, read {i} so far)");
                            }

                            cptr[i + 0] = (char) b[0];
                            cptr[i + 1] = (char) b[1];
                            return chars;
                        }
                        else if (length == 1) {
                            if (s.Read(b, 0, 1) != 1) {
                                throw new EndOfStreamException($"Failed to read 1 byte for 1 char ({length} bytes remaining, last 1 char, read {i} so far)");
                            }

                            cptr[i] = (char) b[0];
                            return chars;
                        }
                        else {
                            return chars;
                        }
                    }
                }
                else if (length == 3) {
                    if (s.Read(b, 0, 3) != 3) {
                        throw new EndOfStreamException("Failed to read 3 bytes for 3 chars (in string len 3)");
                    }

                    // cant be bothered to do the rest xd
                    chars[0] = (char) b[0];
                    chars[1] = (char) b[1];
                    chars[2] = (char) b[2];
                    return chars;
                }
                else if (length == 2) {
                    if (s.Read(b, 0, 2) != 2) {
                        throw new EndOfStreamException("Failed to read 2 bytes for 2 chars (in string len 2)");
                    }

                    chars[0] = (char) b[0];
                    chars[1] = (char) b[1];
                    return chars;
                }
                else if (length == 1) {
                    if (s.Read(b, 0, 1) != 1) {
                        throw new EndOfStreamException("Failed to read 1 byte for a char (in string len 1)");
                    }

                    chars[0] = (char) b[0];
                    return chars;
                }
                else {
                    return chars;
                }
            }
        }

        public unsafe void ReadPtr(byte* dest, int offset, int length) {
            byte[] b = this.buffer8;
            Stream s = this.stream;
            fixed (byte* buf = b) {
                while (length > 7) {
                    if (s.Read(b, 0, 8) != 8) {
                        throw new EndOfStreamException($"Failed to read 8 bytes for 8 chars ({length} bytes remaining)");
                    }

                    *(ulong*) (dest + offset) = *(ulong*) buf;
                    length -= 8;
                    offset += 8;
                }

                if (length > 3) {
                    if (s.Read(b, 0, 4) != 4) {
                        throw new EndOfStreamException($"Failed to read 4 bytes for 4 chars ({length} bytes remaining)");
                    }

                    *(uint*) (dest + offset) = *(uint*) buf;
                    length -= 4;
                    offset += 4;
                }

                if (length == 3) {
                    if (s.Read(b, 0, 3) != 3) {
                        throw new EndOfStreamException($"Failed to read 3 bytes for 3 chars ({length} bytes remaining, last 3 chars)");
                    }

                    dest[offset + 0] = buf[0];
                    dest[offset + 1] = buf[1];
                    dest[offset + 2] = buf[2];
                }
                else if (length == 2) {
                    if (s.Read(b, 0, 2) != 2) {
                        throw new EndOfStreamException($"Failed to read 2 bytes for 2 chars ({length} bytes remaining, last 2 chars)");
                    }

                    dest[offset + 0] = buf[0];
                    dest[offset + 1] = buf[1];
                }
                else if (length == 1) {
                    if (s.Read(b, 0, 1) != 1) {
                        throw new EndOfStreamException($"Failed to read 1 byte for 1 char ({length} bytes remaining, last 1 char)");
                    }

                    dest[offset] = buf[0];
                }
            }
        }

        public void ReadPtr(IntPtr dest, int offset, int length) {
            byte[] b = this.buffer8;
            Stream s = this.stream;
            while (length > 7) {
                if (s.Read(b, 0, 8) != 8) {
                    throw new EndOfStreamException($"Failed to read 8 bytes for 8 chars ({length} bytes remaining)");
                }

                Marshal.WriteInt64(dest, offset, (long) (((ulong) b[0] << 56) +
                                                        ((ulong) b[1] << 48) +
                                                        ((ulong) b[2] << 40) +
                                                        ((ulong) b[3] << 32) +
                                                        ((ulong) b[4] << 24) +
                                                        ((ulong) b[5] << 16) +
                                                        ((ulong) b[6] << 8) +
                                                        ((ulong) b[7] << 0)));
                length -= 8;
                offset += 8;
            }

            if (length > 3) {
                if (s.Read(b, 0, 4) != 4) {
                    throw new EndOfStreamException($"Failed to read 4 bytes for 4 chars ({length} bytes remaining)");
                }

                Marshal.WriteInt32(dest, offset, (int) (((uint) b[0] << 24) +
                                                       ((uint) b[1] << 16) +
                                                       ((uint) b[2] << 8) +
                                                       ((uint) b[3] << 0)));
                length -= 4;
                offset += 4;
            }

            if (length == 3) {
                if (s.Read(b, 0, 3) != 3) {
                    throw new EndOfStreamException($"Failed to read 3 bytes for 3 chars ({length} bytes remaining, last 3 chars)");
                }

                Marshal.WriteByte(dest, offset + 0, b[0]);
                Marshal.WriteByte(dest, offset + 1, b[1]);
                Marshal.WriteByte(dest, offset + 2, b[2]);
            }
            else if (length == 2) {
                if (s.Read(b, 0, 2) != 2) {
                    throw new EndOfStreamException($"Failed to read 2 bytes for 2 chars ({length} bytes remaining, last 2 chars)");
                }

                Marshal.WriteByte(dest, offset + 0, b[0]);
                Marshal.WriteByte(dest, offset + 1, b[1]);
            }
            else if (length == 1) {
                if (s.Read(b, 0, 1) != 1) {
                    throw new EndOfStreamException($"Failed to read 1 byte for 1 char ({length} bytes remaining, last 1 char)");
                }

                Marshal.WriteByte(dest, offset, b[0]);
            }
        }

        public T ReadPrimitive<T>() where T : unmanaged {
            unsafe {
                T t = new T();
                ReadPtr((byte*) &t, 0, sizeof(T));
                return t;
            }
        }

        public T ReadPrimitive<T>(T value) where T : unmanaged {
            unsafe {
                ReadPtr((byte*) &value, 0, sizeof(T));
                return value;
            }
        }
    }
}