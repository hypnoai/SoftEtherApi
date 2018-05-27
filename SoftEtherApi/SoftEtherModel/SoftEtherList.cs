using System.Collections.Generic;

namespace SoftEtherApi.SoftEtherModel
{
    public class SoftEtherList<T> : BaseSoftEtherModel<T> where T : BaseSoftEtherModel<T>, new()
    {
        public List<T> Elements = new List<T>();
    }
}