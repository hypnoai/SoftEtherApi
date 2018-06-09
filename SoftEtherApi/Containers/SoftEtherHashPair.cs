namespace SoftEtherApi.Containers
{
    public struct SoftEtherHashPair
    {
        public byte[] Hash { get; set; }
        public byte[] SaltedHash { get; set; }

        public SoftEtherHashPair(byte[] hash, byte[] saltedHash)
        {
            Hash = hash;
            SaltedHash = saltedHash;
        }
    }
}