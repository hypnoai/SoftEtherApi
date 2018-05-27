using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SoftEtherApi.Extensions;

namespace SoftEtherApi
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

        public static byte[] Serialize(Dictionary<string, (string, dynamic[])> list)
        {
            var memStream = new MemoryStream();
            var writer = new BinaryWriter(memStream);

            writer.WriteInt32BE(list.Count);

            foreach (var el in list)
            {
                var keyBytes = Encoding.ASCII.GetBytes(el.Key);
                writer.WriteInt32BE(keyBytes.Length + 1);
                writer.Write(keyBytes);


                switch (el.Value.Item1)
                {
                    case "int":
                    {
                        writer.WriteInt32BE(0);
                        writer.WriteInt32BE(el.Value.Item2.Length);
                        foreach (var t in el.Value.Item2) 
                            writer.WriteInt32BE((int) t);
                        break;
                    }
                    case "int64":
                    {
                        writer.WriteInt32BE(4);
                        writer.WriteInt32BE(el.Value.Item2.Length);
                        foreach (var t in el.Value.Item2) 
                            writer.WriteInt64BE((long) t);
                        break;
                    }
                    case "string":
                    {
                        writer.WriteInt32BE(2);
                        writer.WriteInt32BE(el.Value.Item2.Length);
                        foreach (var t in el.Value.Item2)
                        {
                            var tBytes = Encoding.ASCII.GetBytes((string) t);
                            writer.WriteInt32BE(tBytes.Length);
                            writer.Write(tBytes);
                        }
                        break;
                    }
                    case "ustring":
                    {
                        writer.WriteInt32BE(3);
                        writer.WriteInt32BE(el.Value.Item2.Length);
                        foreach (var t in el.Value.Item2)
                        {
                            var tBytes = Encoding.UTF8.GetBytes((string) t);
                            writer.WriteInt32BE(tBytes.Length);
                            writer.Write(tBytes);
                        }
                        break;
                    }
                    case "raw":
                    default:
                    {
                        writer.WriteInt32BE(1);
                        writer.WriteInt32BE(el.Value.Item2.Length);
                        foreach (var t in el.Value.Item2)
                        {
                            writer.WriteInt32BE(((byte[]) t).Length);
                            writer.Write((byte[]) t);
                        }
                        break;
                    }
                }
            }

            return memStream.ToArray();
        }

        public static Dictionary<string, List<dynamic>> Deserialize(byte[] body)
        {
            var memStream = new MemoryStream(body, false);
            var reader = new BinaryReader(memStream);

            var count = reader.ReadInt32BE();

            var res = new Dictionary<string, List<dynamic>>();
            for (var i = 0; i < count; i++)
            {
                var keyLen = reader.ReadInt32BE();
                var key = Encoding.ASCII.GetString(reader.ReadBytesRequired(keyLen - 1));
                var keyType = reader.ReadInt32BE();
                var valueCount = reader.ReadInt32BE();

                var list = new List<dynamic>();
                for (var j = 0; j < valueCount; j++)
                {
                    switch (keyType)
                    {
                        case 0:
                        {
                            list.Add(reader.ReadInt32BE());
                            break;
                        }
                        case 1:
                        {
                            var strLen = reader.ReadInt32BE();
                            list.Add(reader.ReadBytesRequired(strLen));
                            break;
                        }
                        case 2:
                        {
                            var strLen = reader.ReadInt32BE();
                            list.Add(Encoding.ASCII.GetString(reader.ReadBytesRequired(strLen)));
                            break;
                        }
                        case 3:
                        {
                            var strLen = reader.ReadInt32BE();
                            list.Add(Encoding.UTF8.GetString(reader.ReadBytesRequired(strLen)));
                            break;
                        }
                        case 4:
                        {
                            list.Add(reader.ReadInt64BE());
                            break;
                        }
                    }
                }

                res.Add(key, list);
            }
            return res;
        }
    }
}