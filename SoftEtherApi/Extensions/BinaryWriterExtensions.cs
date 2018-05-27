using System;
using System.IO;

namespace SoftEtherApi.Extensions
{
    public static class BinaryWriterExtensions
    {
        // Note this MODIFIES THE GIVEN ARRAY then returns a reference to the modified array.
        private static byte[] Reverse(this byte[] b)
        {
            Array.Reverse(b);
            return b;
        }

        public static void WriteUInt16BE(this BinaryWriter binWriter, ushort val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }

        public static void WriteInt16BE(this BinaryWriter binWriter, short val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }

        public static void WriteUInt32BE(this BinaryWriter binWriter, uint val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }

        public static void WriteUInt64BE(this BinaryWriter binWriter, ulong val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }

        public static void WriteInt32BE(this BinaryWriter binWriter, int val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }

        public static void WriteInt64BE(this BinaryWriter binWriter, long val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }
    }
}