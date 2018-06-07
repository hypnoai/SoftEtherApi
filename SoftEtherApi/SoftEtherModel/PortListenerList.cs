using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class PortListenerList : BaseSoftEtherModel<PortListenerList>
    {
        public uint  Enables;
        public uint  Errors;
        public uint  Ports;
    }
}