using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using SoftEtherApi.Containers;
using SoftEtherApi.Infrastructure;

namespace SoftEtherApi.SoftEtherModel
{
    public abstract class BaseSoftEtherModel<T> where T : BaseSoftEtherModel<T>, new()
    {
        public SoftEtherError? Error;

        public bool Valid()
        {
            return !Error.HasValue;
        }

        public static T Deserialize(SoftEtherParameterCollection collection)
        {
            var keyMapping = collection.Select(m => new Tuple<string, SoftEtherParameter>(m.Key.Replace(".", "").Replace("@", ""), m))
                .ToDictionary(m => m.Item1.ToLower(), m => m.Item2);

            var returnVal = new T();
            var valFields = typeof(T).GetFields();

            foreach (var field in valFields)
            {
                var keyName = field.Name.ToLower();
                if (!keyMapping.ContainsKey(keyName))
                    continue;

                var parameter = keyMapping[keyName];
                var fieldType = field.FieldType;

                if (!fieldType.IsArray && fieldType.GetInterface("IList") != null)
                {
                    var tmpVal = Activator.CreateInstance(fieldType);
                    var tmpList = (IList)tmpVal;

                    var elementType = fieldType.GetGenericArguments()[0];

                    foreach (var el in parameter.Value) 
                        tmpList.Add(CastValue(elementType, el));
                    
                    field.SetValue(returnVal, tmpVal);
                }
                else
                {
                    field.SetValue(returnVal, CastValue(fieldType, parameter.Value.FirstOrDefault()));
                }
            }

            return returnVal;
        }

        public static SoftEtherList<T> DeserializeMany(SoftEtherParameterCollection collection)
        {
            var keyMapping = collection.Select(m => new Tuple<string, SoftEtherParameter>(m.Key.Replace(".", "").Replace("@", ""), m))
                .ToDictionary(tuple => tuple.Item1.ToLower(), tuple => tuple.Item2);
            var elementCount = collection.Max(m => m.Value.Count);

            var returnVal = new SoftEtherList<T>();
            var valFields = typeof(T).GetFields();

            for (var i = 0; i < elementCount; i++)
            {
                var tVal = new T();
                foreach (var field in valFields)
                {
                    var keyName = field.Name.ToLower();
                    if (!keyMapping.ContainsKey(keyName))
                        continue;

                    var val = keyMapping[keyName].Value;
                    var fieldType = field.FieldType;

                    if (!fieldType.IsArray && fieldType.GetInterface("IList") != null)
                    {
                        var tmpVal = Activator.CreateInstance(fieldType);
                        var tmpList = (IList) tmpVal;

                        var elementType = fieldType.GetGenericArguments()[0];

                        foreach (var el in val)
                            tmpList.Add(CastValue(elementType, el));

                        field.SetValue(tVal, tmpVal);
                    }
                    else
                    {
                        var el = val.Count == 1 ? val.FirstOrDefault() : val.ElementAtOrDefault(i);
                        field.SetValue(tVal, CastValue(fieldType, el));
                    }
                }

                returnVal.Elements.Add(tVal);
            }

            return returnVal;
        }

        private static object CastValue(Type valType, object val)
        {
            if (valType == typeof(DateTime))
            {
                return SoftEtherConverter.LongToDateTime(Convert.ToInt64(val ?? 0));
            }
            
            if (valType == typeof(TimeSpan))
            {
                return new TimeSpan(0, 0, 0, Convert.ToInt32(val));
            }

            if (valType == typeof(IPAddress))
            {
                return SoftEtherConverter.UIntToIpAddress((uint) val);
            }
            
            if (valType == typeof(X509Certificate))
            {
                return new X509Certificate((byte[])val);
            }

            if (valType == typeof(bool))
            {
                return Convert.ToBoolean(val);
            }

            if (valType == typeof(SoftEtherError?))
            {
                return Enum.ToObject(typeof(SoftEtherError), val);
            }
            
            if (valType == typeof(HubType))
            {
                return Enum.ToObject(typeof(HubType), val);
            }

            return val;
        }
    }
}