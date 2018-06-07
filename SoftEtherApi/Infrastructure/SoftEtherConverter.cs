using System;
using System.Net;

namespace SoftEtherApi.Infrastructure
{
    public static class SoftEtherConverter
    {
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static int HoursTimeOffset { get; set; } = 9; //UTC+9 for JAPAN
        
        public static DateTime LongToDateTime(long val)
        {
            return FromUnixTimeMilliseconds(val).AddHours(HoursTimeOffset).ToLocalTime();
        }

        public static long DateTimeToLong(DateTime val)
        {
            var dateTime = val.AddHours(-HoursTimeOffset);
            return ToUnixTimeMilliseconds(dateTime);
        }

        public static IPAddress UIntToIpAddress(uint val)
        {
            return new IPAddress(val);
        }
        
        public static uint IpAddressToUint(IPAddress val)
        {
            return BitConverter.ToUInt32(val.GetAddressBytes(), 0);
        }

        public static long ToUnixTimeMilliseconds(DateTime val)
        {
            var unixDateTime = (val.ToUniversalTime() - Epoch).TotalMilliseconds;
            return (long)unixDateTime;
        }
        
        public static DateTime FromUnixTimeMilliseconds(long val)
        {
            var timeSpan = TimeSpan.FromMilliseconds(val);
            var localDateTime = Epoch.Add(timeSpan).ToLocalTime();
            return localDateTime;
        }
    }
}