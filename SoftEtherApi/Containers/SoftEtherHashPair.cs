namespace SoftEtherApi.Containers
{
    public struct SoftEtherHashPair
    {
        public byte[] Hash { get; set; }
        public byte[] Secure { get; set; }

        public SoftEtherHashPair(byte[] hash, byte[] secure)
        {
            Hash = hash;
            Secure = secure;
        }
    }
}