using System;
using System.Net;

namespace SoftEtherApi.Infrastructure
{
    public static class SoftEtherConverter
    {
        public static int HoursTimeOffset { get; set; } = 9; //UTC+9 for JAPAN
        
        public static DateTime LongToDateTime(long val)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(val).AddHours(HoursTimeOffset).ToLocalTime().DateTime;
        }

        public static long DateTimeToLong(DateTime val)
        {
            return new DateTimeOffset(val).AddHours(-HoursTimeOffset).ToUnixTimeMilliseconds();
        }

        public static IPAddress UIntToIpAddress(uint val)
        {
            
            return new IPAddress(val);
        }
        
        public static uint IpAddressToUint(IPAddress val)
        {
            return BitConverter.ToUInt32(val.GetAddressBytes(), 0);
        }
    }
}