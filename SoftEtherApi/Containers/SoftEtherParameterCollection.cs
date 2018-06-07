using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SoftEtherApi.Infrastructure;

namespace SoftEtherApi.Containers
{
    public class SoftEtherParameterCollection : IEnumerable<SoftEtherParameter>
    {
        private Dictionary<string, SoftEtherParameter> Parameters { get; set; } =
            new Dictionary<string, SoftEtherParameter>();

        public int Count => Parameters.Count;

        public IEnumerator<SoftEtherParameter> GetEnumerator()
        {
            return Parameters.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Parameters.Values.GetEnumerator();
        }

        public List<string> GetKeys()
        {
            return Parameters.Keys.ToList();
        }

        public bool Contains(string key)
        {
            return Parameters.ContainsKey(key);
        }

        public SoftEtherParameter Get(string key)
        {
            return Parameters[key];
        }

        public void Add(string key, SoftEtherValueType valType, params object[] value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, valType, value));
        }
        
        public void Add(string key, SoftEtherValueType valType, IEnumerable<object> value)
        {
            Add(key, valType, value.ToArray());
        }

        public void Add(string key, params string[] value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.String, value));
        }
        
        public void Add(string key, IEnumerable<string> value)
        {
            Add(key, value.ToArray());
        }

        public void Add(string key, params int[] value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Int, value));
        }
        
        public void Add(string key, IEnumerable<int> value)
        {
            Add(key, value.ToArray());
        }

        public void Add(string key, params uint[] value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Int, value));
        }
        
        public void Add(string key, IEnumerable<uint> value)
        {
            Add(key, value.ToArray());
        }

        public void Add(string key, params long[] value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Int64, value));
        }
        
        public void Add(string key, IEnumerable<long> value)
        {
            Add(key, value.ToArray());
        }

        public void Add(string key, params ulong[] value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Int64, value));
        }
        
        public void Add(string key, IEnumerable<ulong> value)
        {
            Add(key, value.ToArray());
        }

        public void Add(string key, params byte[][] value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Raw, value));
        }
        
        public void Add(string key, IEnumerable<byte[]> value)
        {
            Add(key, value.ToArray());
        }

        public void Add(string key, params bool[] value)
        {
            Add(key, value.Select(m => m ? 1 : 0).ToArray());
        }
        
        public void Add(string key, IEnumerable<bool> value)
        {
            Add(key, value.ToArray());
        }

        public void Add(string key, params IPAddress[] value)
        {
            Add(key, value.Select(SoftEtherConverter.IpAddressToUint).ToArray());
        }
        
        public void Add(string key, IEnumerable<IPAddress> value)
        {
            Add(key, value.ToArray());
        }

        public void Add(string key, params DateTime[] value)
        {
            Add(key, value.Select(SoftEtherConverter.DateTimeToLong).ToArray());
        }
        
        public void Add(string key, IEnumerable<DateTime> value)
        {
            Add(key, value.ToArray());
        }

        public void Add(string key, params DateTime?[] value)
        {
            Add(key, value.Where(m => m.HasValue).Select(m => m.Value).ToArray());
        }
        
        public void Add(string key, IEnumerable<DateTime?> value)
        {
            Add(key, value.ToArray());
        }

        public void RemoveNullParameters()
        {
            foreach (var softEtherParameter in Parameters.Values)
            {
                softEtherParameter.RemoveNullFromValueArray();
            }

            Parameters = Parameters.Where(m => m.Value.HasValues()).ToDictionary(m => m.Key, m => m.Value);
        }
    }
}