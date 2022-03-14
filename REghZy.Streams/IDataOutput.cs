using System;
using System.IO;

namespace REghZy.Streams {
    /// <summary>
    /// An interface for writing primitive data to a stream
    /// <para>
    /// The bytes will be written in the big-endianness format, apart from writing pointer values, which will be
    /// written in your processor architecture's format, which for modern hardware is little-endianness
    /// </para>
    /// </summary>
    public interface IDataOutput {
        /// <summary>
        /// The base stream that this data output will write to
        /// </summary>
        Stream Stream { get; set; }

        /// <summary>
        /// Flushes the data to the stream
        /// </summary>
        void Flush();

        /// <summary>
        /// Closes the stream
        /// </summary>
        void Close();

        /// <summary>
        /// Writes the given number of bytes, starting at the given offset, from the given buffer
        /// </summary>
        /// <param name="src">The buffer to write data from</param>
        /// <param name="offset">The index to start reading from the buffer</param>
        /// <param name="count">The number of bytes to write</param>
        void Write(byte[] src, int offset, int count);

        /// <summary>
        /// Writes the bytes in the given buffer, starting at the given offset
        /// </summary>
        /// <param name="src">The buffer to write data from</param>
        /// <param name="offset">The index to start reading from the buffer</param>
        void Write(byte[] src, int offset = 0);

        /// <summary>
        /// Writes a boolean value (1 byte)
        /// </summary>
        /// <param name="val"></param>
        void WriteBoolean(bool val);

        /// <summary>
        /// Writes an enum value as a byte
        /// </summary>
        /// <typeparam name="TEnum">The enum type whose size is 1 byte big</typeparam>
        /// <param name="value">The value to write</param>
        void WriteEnum8<TEnum>(TEnum value) where TEnum : unmanaged, Enum;

        /// <summary>
        /// Writes an enum value as a ushort (2 bytes)
        /// </summary>
        /// <typeparam name="TEnum">The enum type whose size is atleast 2 bytes big</typeparam>
        /// <param name="value">The value to write</param>
        void WriteEnum16<TEnum>(TEnum value) where TEnum : unmanaged, Enum;

        /// <summary>
        /// Writes an enum value as a uint (4 bytes)
        /// </summary>
        /// <typeparam name="TEnum">The enum type whose size is atleast 4 bytes big</typeparam>
        /// <param name="value">The value to write</param>
        void WriteEnum32<TEnum>(TEnum value) where TEnum : unmanaged, Enum;

        /// <summary>
        /// Writes an enum value as a ulong value (8 bytes)
        /// </summary>
        /// <typeparam name="TEnum">The enum type whose size is atleast 8 bytes big</typeparam>
        /// <param name="value">The value to write</param>
        void WriteEnum64<TEnum>(TEnum value) where TEnum : unmanaged, Enum;

        /// <summary>
        /// Writes a single unsigned byte (0-255)
        /// </summary>
        /// <param name="value"></param>
        void WriteByte(byte value);

        /// <summary>
        /// Writes a single signed byte (-128 to 127)
        /// </summary>
        /// <param name="value"></param>
        void WriteSByte(sbyte value);

        /// <summary>
        /// Writes a signed short (2 bytes) (-32768 to 32767)
        /// </summary>
        /// <param name="value"></param>
        void WriteShort(short value);

        /// <summary>
        /// Writes a short (2 bytes) (0 to 65535)
        /// </summary>
        /// <param name="value"></param>
        void WriteUShort(ushort value);

        /// <summary>
        /// Writes an integer (4 bytes) (-2,147,483,648 to 2,147,483,647)
        /// </summary>
        /// <param name="value"></param>
        void WriteInt(int value);

        /// <summary>
        /// Writes an unsigned integer (4 bytes) (0 to 4,294,967,295)
        /// </summary>
        /// <param name="value"></param>
        void WriteUInt(uint value);

        /// <summary>
        /// Writes a signed long (8 bytes) (-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807)
        /// </summary>
        /// <param name="value"></param>
        void WriteLong(long value);

        /// <summary>
        /// Writes an unsigned long (8 bytes) (0 to 18,446,744,073,709,551,615)
        /// </summary>
        /// <param name="value"></param>
        void WriteULong(ulong value);

        /// <summary>
        /// Writes a floating point number (4 bytes)
        /// </summary>
        /// <param name="value"></param>
        void WriteFloat(float value);

        /// <summary>
        /// Writes a double percision floating point number (8 bytes)
        /// </summary>
        /// <param name="value"></param>
        void WriteDouble(double value);

        /// <summary>
        /// Writes a char (2 bytes, exact same as <see cref="WriteUShort(ushort)"/>)
        /// </summary>
        /// <param name="value"></param>
        void WriteCharUTF16(char value);

        /// <summary>
        /// Writes a char (1 byte, exact same as <see cref="WriteByte(byte)"/>)
        /// </summary>
        /// <param name="value"></param>
        void WriteCharUTF8(char value);

        /// <summary>
        /// Writes all of the chars in the given string
        /// </summary>
        /// <param name="value">The string to write</param>
        void WriteStringUTF16(string value);

        /// <summary>
        /// Writes all of the chars in the given string
        /// </summary>
        /// <param name="value">The string to write</param>
        void WriteStringUTF8(string value);

        /// <summary>
        /// Writes all of the chars in the given string. This writes 2 bytes per char;
        /// first the high byte (bit 9-16), and then the low byte (bit 1-8), meaning 2 bytes per char
        /// </summary>
        /// <param name="chars">The chars to write</param>
        void WriteCharsUTF16(char[] chars);

        /// <summary>
        /// Writes all of the chars in the given string. This only writes the low byte of
        /// the char (bit 1-8), and does not send the high byte. Meaning, only 1 byte per char
        /// </summary>
        /// <param name="chars">The chars to write</param>
        void WriteCharsUTF8(char[] chars);

        /// <summary>
        /// Writes '2 * length' bytes from the given pointer (starting, in the pointer, at the given offset)
        /// </summary>
        /// <param name="src">The pointer to get the chars from</param>
        /// <param name="offset">The offset within the pointer (usually this starts at 0)</param>
        /// <param name="length">The number of characters to write (not bytes, characters)</param>
        unsafe void WritePtrUTF16(char* src, int offset, int length);

        /// <summary>
        /// Writes 'length' bytes from the given pointer (starting, in the pointer, at the given offset)
        /// </summary>
        /// <param name="src">The pointer to get the chars from</param>
        /// <param name="offset">The offset within the pointer (usually this starts at 0)</param>
        /// <param name="length">The number of characters/bytes to write</param>
        unsafe void WritePtrUTF8(char* src, int offset, int length);

        /// <summary>
        /// Writes 'length' bytes from the given pointer (starting, in the pointer, at the given offset)
        /// <para>
        /// The data written will be in your processor architecture's endianness, which for modern hardware is little-endianness.
        /// However, most of the functions in this library write in big-endianness (e.g <see cref="WriteUInt"/>)
        /// </para>
        /// </summary>
        /// <param name="src">The pointer to a buffer of characters</param>
        /// <param name="offset">The offset within the pointer (usually this starts at 0)</param>
        /// <param name="length">The number of characters/bytes to write</param>
        unsafe void WritePtr(byte* src, int offset, int length);

        /// <summary>
        /// Writes 'length' bytes from the given pointer (starting, in the pointer, at the given offset)
        /// <para>
        /// The data written will be in your processor architecture's endianness, which for modern hardware is little-endianness.
        /// However, most of the functions in this library write in big-endianness (e.g <see cref="WriteUInt"/>)
        /// </para>
        /// </summary>
        /// <param name="src">The pointer to a buffer of characters</param>
        /// <param name="offset">The offset within the pointer (usually this starts at 0)</param>
        /// <param name="length">The number of characters/bytes to write</param>
        void WritePtr(IntPtr src, int offset, int length);

        /// <summary>
        /// Writes a blittable value/object, where all of the value's bytes will be written
        /// <para>
        /// The data written will be in your processor architecture's endianness, which for modern hardware is little-endianness.
        /// However, most of the functions in this library write in big-endianness (e.g <see cref="WriteUInt"/>)
        /// </para>
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <typeparam name="T">The blittable type</typeparam>
        void WritePrimitive<T>(T value) where T : unmanaged;
    }
}