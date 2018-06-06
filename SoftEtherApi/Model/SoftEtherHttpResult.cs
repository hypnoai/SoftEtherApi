using System.Collections.Generic;

namespace SoftEtherApi.Model
{
    public class SoftEtherHttpResult
    {
        public int code;
        public Dictionary<string, string> headers;
        public int length; 
        public byte[] body;
    }
}