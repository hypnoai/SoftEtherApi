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

        public static void WriteUInt16BE(this BinaryWriter binWriter, UInt16 val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }

        public static void WriteInt16BE(this BinaryWriter binWriter, Int16 val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }

        public static void WriteUInt32BE(this BinaryWriter binWriter, UInt32 val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }
        
        public static void WriteUInt64BE(this BinaryWriter binWriter, UInt64 val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }

        public static void WriteInt32BE(this BinaryWriter binWriter, Int32 val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }
        
        public static void WriteInt64BE(this BinaryWriter binWriter, Int64 val)
        {
            binWriter.Write(BitConverter.GetBytes(val).Reverse());
        }
    }
}