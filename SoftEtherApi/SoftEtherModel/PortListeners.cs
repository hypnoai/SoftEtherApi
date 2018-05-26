using System.Collections.Generic;

namespace SoftEtherApi.SoftEtherModel
{
    public class PortListeners : BaseSoftEtherModel<PortListeners>
    {
        public List<int> Enables;
        public List<int> Errors;
        public List<int> Ports;
    }
}

