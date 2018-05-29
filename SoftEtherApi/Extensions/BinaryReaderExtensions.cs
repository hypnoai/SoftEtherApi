using System;
using System.IO;
using System.Text;

namespace SoftEtherApi.Extensions
{
    public static class BinaryReaderExtensions
    {
        // Note this MODIFIES THE GIVEN ARRAY then returns a reference to the modified array.
        private static byte[] Reverse(this byte[] b)
        {
            Array.Reverse(b);
            return b;
        }

        public static ushort ReadUInt16BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt16(binRdr.ReadBytesRequired(sizeof(ushort)).Reverse(), 0);
        }

        public static short ReadInt16BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt16(binRdr.ReadBytesRequired(sizeof(short)).Reverse(), 0);
        }

        public static uint ReadUInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt32(binRdr.ReadBytesRequired(sizeof(uint)).Reverse(), 0);
        }

        public static ulong ReadUInt64BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt64(binRdr.ReadBytesRequired(sizeof(ulong)).Reverse(), 0);
        }

        public static int ReadInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt32(binRdr.ReadBytesRequired(sizeof(int)).Reverse(), 0);
        }

        public static long ReadInt64BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt64(binRdr.ReadBytesRequired(sizeof(long)).Reverse(), 0);
        }

        public static string ReadStringBE(this BinaryReader binRdr, int len)
        {
            return BitConverter.ToString(binRdr.ReadBytesRequired(len).Reverse(), 0);
        }

        public static byte[] ReadBytesRequired(this BinaryReader binRdr, int byteCount)
        {
            var result = binRdr.ReadBytes(byteCount);

            if (result.Length != byteCount)
                throw new EndOfStreamException($"{byteCount} bytes required from stream, but only {result.Length} returned.");

            return result;
        }

        public static string ReadLine(this BinaryReader reader)
        {
            var result = new StringBuilder();
            char character;
            while ((character = reader.ReadChar()) != '\n')
                if (character != '\r' && character != '\n')
                    result.Append(character);

            return result.ToString();
        }
    }
}