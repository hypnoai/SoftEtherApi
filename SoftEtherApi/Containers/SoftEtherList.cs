using System.Collections.Generic;
using SoftEtherApi.Model;

namespace SoftEtherApi.Containers
{
    public class SoftEtherList<T> : BaseSoftEtherModel<T> where T : BaseSoftEtherModel<T>, new()
    {
        public List<T> Elements = new List<T>();
    }
}