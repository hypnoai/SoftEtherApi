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
        private Dictionary<string, SoftEtherParameter> Parameters { get; set; } = new Dictionary<string, SoftEtherParameter>();
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

        public void Add(string key, SoftEtherValueType valType, object value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, valType, value));            
        }
        
        public void Add(string key, string value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.String, value));            
        }
        
        public void Add(string key, IPAddress value)
        {
            Add(key, SoftEtherConverter.IpAddressToUint(value));           
        }
        
        public void Add(string key, int value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Int, value));            
        }
        
        public void Add(string key, uint value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Int, value));            
        }
        
        public void Add(string key, bool value)
        {
            Add(key, value ? 1 : 0);            
        }
        
        public void Add(string key, long value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Int64, value));            
        }
        
        public void Add(string key, ulong value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Int64, value));            
        }
        
        public void Add(string key, DateTime value)
        {
            Add(key, SoftEtherConverter.DateTimeToLong(value));            
        }
        
        public void Add(string key, DateTime? value)
        {
            if (!value.HasValue)
                return;
            
            Add(key, value.Value);            
        }
        
        public void Add(string key, byte[] value)
        {
            Parameters.Add(key, new SoftEtherParameter(key, SoftEtherValueType.Raw, value));            
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