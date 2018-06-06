using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SoftEtherApi.Infrastructure;

namespace SoftEtherApi.Containers
{
    public class SoftEtherParameterCollection : IEnumerable<SoftEtherParameter>
    {
        private List<SoftEtherParameter> Parameters { get; set; } = new List<SoftEtherParameter>();
        public int Count => Parameters.Count;

        public IEnumerator<SoftEtherParameter> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        public void Add(string key, string valType, object value)
        {
            Parameters.Add(new SoftEtherParameter(key, valType, value));            
        }
        
        public void Add(string key, string value)
        {
            Parameters.Add(new SoftEtherParameter(key, "string", value));            
        }
        
        public void Add(string key, int value)
        {
            Parameters.Add(new SoftEtherParameter(key, "int", value));            
        }
        
        public void Add(string key, bool value)
        {
            Add(key, value ? 1 : 0);            
        }
        
        public void Add(string key, long value)
        {
            Parameters.Add(new SoftEtherParameter(key, "int64", value));            
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
            Parameters.Add(new SoftEtherParameter(key, "raw", value));            
        }

        public void RemoveNullParameters()
        {
            foreach (var softEtherParameter in Parameters)
            {
                softEtherParameter.RemoveNullFromValueArray();
            }

            Parameters = Parameters.Where(m => !m.ValueIsNull()).ToList();
        }
    }
}