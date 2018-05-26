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

        public static UInt16 ReadUInt16BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt16(binRdr.ReadBytesRequired(sizeof(UInt16)).Reverse(), 0);
        }

        public static Int16 ReadInt16BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt16(binRdr.ReadBytesRequired(sizeof(Int16)).Reverse(), 0);
        }

        public static UInt32 ReadUInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt32(binRdr.ReadBytesRequired(sizeof(UInt32)).Reverse(), 0);
        }

        public static UInt64 ReadUInt64BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt64(binRdr.ReadBytesRequired(sizeof(UInt64)).Reverse(), 0);
        }

        public static Int32 ReadInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt32(binRdr.ReadBytesRequired(sizeof(Int32)).Reverse(), 0);
        }

        public static Int64 ReadInt64BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt64(binRdr.ReadBytesRequired(sizeof(Int64)).Reverse(), 0);
        }

        public static string ReadStringBE(this BinaryReader binRdr, int len)
        {
            return BitConverter.ToString(binRdr.ReadBytesRequired(len).Reverse(), 0);
        }

        public static byte[] ReadBytesRequired(this BinaryReader binRdr, int byteCount)
        {
            var result = binRdr.ReadBytes(byteCount);

            if (result.Length != byteCount)
                throw new EndOfStreamException(string.Format("{0} bytes required from stream, but only {1} returned.",
                    byteCount, result.Length));

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