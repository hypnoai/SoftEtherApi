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

        public static byte[] Serialize(Dictionary<string, (string, object[])> list)
        {
            var memStream = new MemoryStream();
            var writer = new BinaryWriter(memStream);

            writer.WriteUInt32BE(Convert.ToUInt32(list.Count));

            foreach (var el in list)
            {
                var keyBytes = Encoding.ASCII.GetBytes(el.Key);
                writer.WriteUInt32BE(Convert.ToUInt32(keyBytes.Length + 1));
                writer.Write(keyBytes);


                switch (el.Value.Item1)
                {
                    case "int":
                    {
                        writer.WriteUInt32BE(0);
                        writer.WriteUInt32BE(Convert.ToUInt32(el.Value.Item2.Length));
                        foreach (var t in el.Value.Item2) 
                            writer.WriteUInt32BE(Convert.ToUInt32(t));
                        break;
                    }
                    case "int64":
                    {
                        writer.WriteUInt32BE(4);
                        writer.WriteUInt32BE(Convert.ToUInt32(el.Value.Item2.Length));
                        foreach (var t in el.Value.Item2) 
                            writer.WriteUInt64BE(Convert.ToUInt64(t));
                        break;
                    }
                    case "string":
                    {
                        writer.WriteUInt32BE(2);
                        writer.WriteUInt32BE(Convert.ToUInt32(el.Value.Item2.Length));
                        foreach (var t in el.Value.Item2)
                        {
                            var tBytes = Encoding.ASCII.GetBytes((string) t);
                            writer.WriteUInt32BE(Convert.ToUInt32(tBytes.Length));
                            writer.Write(tBytes);
                        }
                        break;
                    }
                    case "ustring":
                    {
                        writer.WriteUInt32BE(3);
                        writer.WriteUInt32BE(Convert.ToUInt32(el.Value.Item2.Length));
                        foreach (var t in el.Value.Item2)
                        {
                            var tBytes = Encoding.UTF8.GetBytes((string) t);
                            writer.WriteUInt32BE(Convert.ToUInt32(tBytes.Length));
                            writer.Write(tBytes);
                        }
                        break;
                    }
                    case "raw":
                    default:
                    {
                        writer.WriteUInt32BE(1);
                        writer.WriteUInt32BE(Convert.ToUInt32(el.Value.Item2.Length));
                        foreach (var t in el.Value.Item2)
                        {
                            writer.WriteUInt32BE(Convert.ToUInt32(((byte[]) t).Length));
                            writer.Write((byte[]) t);
                        }
                        break;
                    }
                }
            }

            return memStream.ToArray();
        }

        public static Dictionary<string, List<object>> Deserialize(byte[] body)
        {
            var memStream = new MemoryStream(body, false);
            var reader = new BinaryReader(memStream);

            var count = reader.ReadUInt32BE();

            var res = new Dictionary<string, List<object>>();
            for (var i = 0; i < count; i++)
            {
                var keyLen = reader.ReadInt32BE();
                var key = Encoding.ASCII.GetString(reader.ReadBytesRequired(keyLen - 1));
                var keyType = reader.ReadUInt32BE();
                var valueCount = reader.ReadUInt32BE();

                var list = new List<object>();
                for (var j = 0; j < valueCount; j++)
                {
                    switch (keyType)
                    {
                        case 0:
                        {
                            list.Add(reader.ReadUInt32BE());
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
                            list.Add(reader.ReadUInt64BE());
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