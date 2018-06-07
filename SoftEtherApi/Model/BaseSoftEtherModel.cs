using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SoftEtherApi.Containers;
using SoftEtherApi.Infrastructure;

namespace SoftEtherApi.Model
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

                var val = keyMapping[keyName].Value;

                SetValueForField(field, val, returnVal);
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

        private static void SetValueForField(FieldInfo valueField, IReadOnlyCollection<object> rawValue, T resultValue, int i = 0)
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