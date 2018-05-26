using System;

namespace SoftEtherApi
{
    internal class Program
    {
        public static void Main()
        {
            var ip = "";
            ushort port = 5555;
            var pw = "";

            var hubName = "";
            var userName = "";

            using (var softEther = new SoftEther(ip, port))
            {
                var connectResult = softEther.Connect();
                if (!connectResult.Valid())
                {
                    Console.WriteLine(connectResult.Error);
                    return;
                }

                var authResult = softEther.Authenticate(pw);
                if (!authResult.Valid())
                {
                    Console.WriteLine(authResult.Error);
                    return;
                }

                var user = softEther.HubApi.GetUser(hubName, userName);
                Console.WriteLine(user.Valid() ? "Success" : user.Error.ToString());
            }
        }
    }
}