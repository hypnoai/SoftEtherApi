using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SoftEtherApi.Containers;
using SoftEtherApi.Model;

namespace SoftEtherApi.Infrastructure
{
    public static class ModelDeserializer
    {
        public static string FilterKeyName(string val)
        {
            return val.Replace(".", "")
                .Replace("@", "_")
                .Replace(":", "_").Trim();
        }

        public static Dictionary<string, SoftEtherParameter> CreateKeyMapping(SoftEtherParameterCollection collection)
        {
            return collection.Select(m => new Tuple<string, SoftEtherParameter>(FilterKeyName(m.Key), m))
                .ToDictionary(m => m.Item1.ToLower(), m => m.Item2);
        }

        public static T Deserialize<T>(SoftEtherParameterCollection collection) where T : new()
        {
            var keyMapping = CreateKeyMapping(collection);

            var returnVal = new T();
            var valFields = typeof(T).GetFields();

            foreach (var field in valFields)
            {
                var keyName = field.Name.ToLower();
                if (!keyMapping.ContainsKey(keyName))
                    continue;

                var val = keyMapping[keyName].Value;

                SetValueForField(field, val, returnVal);
            }

            return returnVal;
        }

        public static SoftEtherList<T> DeserializeMany<T>(SoftEtherParameterCollection collection, bool moreThanOne) where T : BaseSoftEtherModel<T>, new()
        {
            var keyMapping = CreateKeyMapping(collection);

            if (keyMapping.ContainsKey("error") && keyMapping["error"].Value.Count <= 1)
            {
                return new SoftEtherList<T>
                {
                    Error = (SoftEtherError)Enum.ToObject(typeof(SoftEtherError), keyMapping["error"].Value[0])
                };
            }

            var elementCount = collection.Max(m => m.Value.Count);

            var returnVal = new SoftEtherList<T>();
            
            //if we expect more than one property to be filled but only get one
            if (!moreThanOne || collection.Count <= 1)
                return returnVal;
            
            var valFields = typeof(T).GetFields();

            for (var i = 0; i < elementCount; i++)
            {
                var elementVal = new T();
                foreach (var field in valFields)
                {
                    var keyName = field.Name.ToLower();
                    if (!keyMapping.ContainsKey(keyName))
                        continue;

                    var val = keyMapping[keyName].Value;
                    SetValueForField(field, val, elementVal, i);
                }

                returnVal.Elements.Add(elementVal);
            }

            return returnVal;
        }

        private static void SetValueForField<T>(FieldInfo valueField, IReadOnlyCollection<object> rawValue, T resultValue, int i = 0)
        {
            var fieldType = valueField.FieldType;

            if (!fieldType.IsArray && fieldType.GetInterface("IList") != null)
            {
                var tmpVal = Activator.CreateInstance(fieldType);
                var tmpList = (IList) tmpVal;

                var elementType = fieldType.GetGenericArguments()[0];

                foreach (var el in rawValue)
                    tmpList.Add(CastValue(elementType, el));

                valueField.SetValue(resultValue, tmpVal);
            }
            else if (fieldType.GetInterfaces().Contains(typeof(ISoftEtherCollection)))
            {
                var tmpVal = Activator.CreateInstance(fieldType);
                var tmpList = (ISoftEtherCollection) tmpVal;

                foreach (var el in rawValue)
                    tmpList.Add(el);

                valueField.SetValue(resultValue, tmpVal);
            }
            else
            {
                var el = rawValue.Count == 1 ? rawValue.FirstOrDefault() : rawValue.ElementAtOrDefault(i);
                valueField.SetValue(resultValue, CastValue(fieldType, el));
            }
        }

        private static object CastValue(Type valType, object val)
        {
            if (valType == null)
                return val;

            //If the type is Nullable, get the underlying type
            valType = Nullable.GetUnderlyingType(valType) ?? valType;
            
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

            if (valType.IsEnum)
            {
                return Enum.ToObject(valType, val);
            }
            
            if (valType == typeof(HubType))
            {
                return Enum.ToObject(typeof(HubType), val);
            }

            return val;
        }
    }
}