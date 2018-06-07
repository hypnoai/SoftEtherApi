using System.Collections;
using System.Collections.Generic;
using SoftEtherApi.Model;

namespace SoftEtherApi.Containers
{
    public class SoftEtherList<T> : BaseSoftEtherModel<T>, IEnumerable<T> where T : BaseSoftEtherModel<T>, new()
    {
        public List<T> Elements = new List<T>();

        public void Add(T el)
        {
            Elements.Add(el);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}