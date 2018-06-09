using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SoftEtherApi.Extensions;

namespace SoftEtherApi.Containers
{
    public static class SoftEtherProtocol
    {
        public static byte[] SerializeInt(int val)
        {
            return BitConverter.GetBytes(val).Reverse().ToArray();
        }

        public static int DeserializeInt(byte[] val)
        {
            return BitConverter.ToInt32(val.Reverse().ToArray(), 0);
        }

        public static byte[] Serialize(SoftEtherParameterCollection list)
        {
            var memStream = new MemoryStream();
            var writer = new BinaryWriter(memStream);

            writer.WriteUInt32BE(Convert.ToUInt32(list.Count));

            foreach (var parameter in list)
            {
                var keyBytes = Encoding.ASCII.GetBytes(parameter.Key);
                writer.WriteUInt32BE(Convert.ToUInt32(keyBytes.Length + 1));
                writer.Write(keyBytes);


                writer.WriteUInt32BE((uint)parameter.ValueType);
                writer.WriteUInt32BE(Convert.ToUInt32(parameter.Value.Count));
                
                switch (parameter.ValueType)
                {
                    case SoftEtherValueType.Int:
                    {
                        
                        foreach (var t in parameter.Value) 
                            writer.WriteUInt32BE(Convert.ToUInt32(t));
                        break;
                    }
                    case SoftEtherValueType.Raw:
                    {
                        foreach (var t in parameter.Value)
                        {
                            writer.WriteUInt32BE(Convert.ToUInt32(((byte[]) t).Length));
                            writer.Write((byte[]) t);
                        }
                        break;
                    }
                    case SoftEtherValueType.String:
                    {
                        foreach (var t in parameter.Value)
                        {
                            var tBytes = Encoding.ASCII.GetBytes((string) t);
                            writer.WriteUInt32BE(Convert.ToUInt32(tBytes.Length));
                            writer.Write(tBytes);
                        }
                        break;
                    }
                    case SoftEtherValueType.UnicodeString:
                    {
                        foreach (var t in parameter.Value)
                        {
                            var tBytes = Encoding.UTF8.GetBytes((string) t);
                            writer.WriteUInt32BE(Convert.ToUInt32(tBytes.Length));
                            writer.Write(tBytes);
                        }
                        break;
                    }
                    case SoftEtherValueType.Int64:
                    {
                        foreach (var t in parameter.Value) 
                            writer.WriteUInt64BE(Convert.ToUInt64(t));
                        break;
                    }
                    default:
                        throw new ArgumentException("ValueType is not valid");
                }
            }

            return memStream.ToArray();
        }
        
        public static SoftEtherParameterCollection Deserialize(byte[] body)
        {
            var memStream = new MemoryStream(body, false);
            var reader = new BinaryReader(memStream);

            var count = reader.ReadUInt32BE();

            var res = new SoftEtherParameterCollection();
            for (var i = 0; i < count; i++)
            {
                var keyLen = reader.ReadInt32BE();
                var key = Encoding.ASCII.GetString(reader.ReadBytesRequired(keyLen - 1));
                var valueType = (SoftEtherValueType)reader.ReadUInt32BE();
                var valueCount = reader.ReadUInt32BE();

                var list = new List<object>();
                for (var j = 0; j < valueCount; j++)
                {
                    switch (valueType)
                    {
                        case SoftEtherValueType.Int:
                        {
                            list.Add(reader.ReadUInt32BE());
                            break;
                        }
                        case SoftEtherValueType.Raw:
                        {
                            var strLen = reader.ReadInt32BE();
                            list.Add(reader.ReadBytesRequired(strLen));
                            break;
                        }
                        case SoftEtherValueType.String:
                        {
                            var strLen = reader.ReadInt32BE();
                            list.Add(Encoding.ASCII.GetString(reader.ReadBytesRequired(strLen)));
                            break;
                        }
                        case SoftEtherValueType.UnicodeString:
                        {
                            //softether adds a additional 0-byte to every string. For future sake, just trim it instead of reading a byte less
                            var strLen = reader.ReadInt32BE();
                            list.Add(Encoding.UTF8.GetString(reader.ReadBytesRequired(strLen)).TrimEnd('\0'));
                            break;
                        }
                        case SoftEtherValueType.Int64:
                        {
                            list.Add(reader.ReadUInt64BE());
                            break;
                        }
                        default:
                            throw new ArgumentException("ValueType is not valid");
                    }
                }

                res.Add(key, valueType, list);
            }
            return res;
        }
    }
}