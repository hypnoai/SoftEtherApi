using System.Collections;
using System.Collections.Generic;
using SoftEtherApi.Model;

namespace SoftEtherApi.Containers
{
    public class SoftEtherList<T> : BaseSoftEtherModel<T>, IList<T> where T : BaseSoftEtherModel<T>, new()
    {
        public readonly List<T> Elements = new List<T>();

        public void Add(T el)
        {
            Elements.Add(el);
        }

        public void Clear()
        {
            Elements.Clear();
        }

        public bool Contains(T item)
        {
            return Elements.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Elements.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return Elements.Remove(item);
        }

        public int Count => Elements.Count;
        public bool IsReadOnly => false;

        public IEnumerator<T> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return Elements.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Elements.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Elements.RemoveAt(index);
        }

        public T this[int index]
        {
            get => Elements[index];
            set => Elements[index] = value;
        }
    }
}