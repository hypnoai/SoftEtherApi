using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SoftEtherApi.SoftEtherModel
{
    public abstract class BaseSoftEtherModel<T> where T : BaseSoftEtherModel<T>, new()
    {
        public SoftEtherError? Error;

        public bool Valid()
        {
            return !Error.HasValue;
        }

        public static T Deserialize(Dictionary<string, List<dynamic>> value)
        {
            var keyMapping = value.Keys.Select(m => (m.Replace(".", "").Replace("@", ""), m))
                .ToDictionary(tuple => tuple.Item1.ToLower(), tuple => tuple.Item2);

            var returnVal = new T();
            var valFields = typeof(T).GetFields();

            foreach (var field in valFields)
            {
                var keyName = field.Name.ToLower();
                if (!keyMapping.ContainsKey(keyName))
                    continue;

                var val = value[keyMapping[keyName]];
                var fieldType = field.FieldType;

                if (!fieldType.IsArray && fieldType.GetInterface("IList") != null)
                {
                    var tmpVal = Activator.CreateInstance(fieldType);
                    var tmpList = tmpVal as IList;

                    var elementType = fieldType.GetGenericArguments()[0];

                    foreach (var el in val) 
                        tmpList.Add(CastValue(elementType, el));
                    
                    field.SetValue(returnVal, tmpVal);
                }
                else
                {
                    field.SetValue(returnVal, CastValue(fieldType, val.FirstOrDefault()));
                }
            }

            return returnVal;
        }

        public static SoftEtherList<T> DeserializeMany(Dictionary<string, List<dynamic>> value)
        {
            var keyMapping = value.Keys.Select(m => (m.Replace(".", "").Replace("@", ""), m))
                .ToDictionary(tuple => tuple.Item1.ToLower(), tuple => tuple.Item2);
            var elementCount = value.Values.Max(m => m.Count);

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

                    var val = value[keyMapping[keyName]];
                    var fieldType = field.FieldType;

                    if (!fieldType.IsArray && fieldType.GetInterface("IList") != null)
                    {
                        var tmpVal = Activator.CreateInstance(fieldType);
                        var tmpList = tmpVal as IList;

                        var elementType = fieldType.GetGenericArguments()[0];

                        foreach (var el in val) tmpList.Add(CastValue(elementType, el));

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
                return SoftEther.LongToDateTime(Convert.ToInt64(val ?? 0));
            }

            if (valType == typeof(IPAddress))
            {
                return new IPAddress((uint)val);
            }

            if (valType == typeof(bool))
            {
                return Convert.ToBoolean(val);
            }

            if (valType == typeof(SoftEtherError?))
            {
                return Enum.ToObject(typeof(SoftEtherError), val);
            }

            return val;
        }
    }
}