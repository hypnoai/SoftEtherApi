# SoftEtherApi
C# .net API to remote control SoftEther VPN Server

# nuget
https://www.nuget.org/packages/SoftEtherApi/
``` nuget
Install-Package SoftEtherApi
```

# Examples

## Program.cs
```c#
using System;
using SoftEtherApi;

namespace TestProgram
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
```

## Connect and authenticate with SoftEther
```c#
var ip = "";
ushort port = 5555; //one of the ports out of the Listener List (443, 992, 1194, 5555)
var pw = "";

using (var softEther = new SoftEther(ip, port))
{
    var connectResult = softEther.Connect();
    if (!connectResult.Valid())
    {
        Console.Write("Could not Connect: ");
        Console.WriteLine(connectResult.Error);
        return;
    }

    var authResult = softEther.Authenticate(pw);
    if (!authResult.Valid())
    {
        Console.Write("Could not authenticate: ");
        Console.WriteLine(authResult.Error);
        return;
    }
}
```

## Connect and authenticate with specific hub
```c#
var ip = "";
ushort port = 5555; //one of the ports out of the Listener List (443, 992, 1194, 5555)
var hubPw = "";

using (var softEther = new SoftEther(ip, port))
{
    var connectResult = softEther.Connect();
    var authResult = softEther.Authenticate(hubPw, "testHub");
}
```

## Get user by name
```c#
var ip = "";
ushort port = 5555; //one of the ports out of the Listener List (443, 992, 1194, 5555)
var pw = "";

using (var softEther = new SoftEther(ip, port))
{
    var connectResult = softEther.Connect();
    var authResult = softEther.Authenticate(pw);

    var user = softEther.HubApi.GetUser("testHub", "testUser");
    Console.WriteLine(user.Valid() ? "Success" : user.Error.ToString());
}
```

## Create user
```c#
var ip = "";
ushort port = 5555; //one of the ports out of the Listener List (443, 992, 1194, 5555)
var pw = "";

using (var softEther = new SoftEther(ip, port))
{
    var connectResult = softEther.Connect();
    var authResult = softEther.Authenticate(pw);

    var user = softEther.HubApi.CreateUser("testHub", "testUser", "userPw");
    Console.WriteLine(user.Valid() ? "Success" : user.Error.ToString());
}
```

## Create online hub
```c#
var ip = "";
ushort port = 5555; //one of the ports out of the Listener List (443, 992, 1194, 5555)
var pw = "";

using (var softEther = new SoftEther(ip, port))
{
    var connectResult = softEther.Connect();
    var authResult = softEther.Authenticate(pw);

    var hub = softEther.HubApi.Create("testHub", "hubPw", true); //true: set hub online
    Console.WriteLine(user.Valid() ? "Success" : hub.Error.ToString());
}
```

## Create hub and set it online
```c#
var ip = "";
ushort port = 5555; //one of the ports out of the Listener List (443, 992, 1194, 5555)
var pw = "";

using (var softEther = new SoftEther(ip, port))
{
    var connectResult = softEther.Connect();
    var authResult = softEther.Authenticate(pw);

    var hub = softEther.HubApi.Create("testHub", "hubPw", true); //true: set hub online
    Console.WriteLine(user.Valid() ? "Success" : hub.Error.ToString());
}
```

## Activate SecureNat and replace accessList to allow network only
```c#
var ip = "";
ushort port = 5555; //one of the ports out of the Listener List (443, 992, 1194, 5555)
var pw = "";

using (var softEther = new SoftEther(ip, port))
{
    var connectResult = softEther.Connect();
    var authResult = softEther.Authenticate(pw);

    var network = "192.168.178.0";
    var networkSubnet = "255.255.255.0";
    var hubName = "testHub";
    
    var natOnline = softEther.HubApi.EnableSecureNat(hubName);
    
    //Add route from secureNat to network for clients
    var natOptions = softEther.HubApi.GetSecureNatOptions(hubName);
    var pushRoutesResult = softEther.HubApi.SetSecureNatDhcpPushRoutes(hubName, new DhcpRouteCollection
    {
        {network, networkSubnet, natOptions.DhcpGatewayAddress.ToString()}
    });
    
    //Replace accessList 
    //Allow access only from secureNat to network. VPN-Clients cannot talk with eachother and access outside of the network is denied
    var networkAclList = softEther.HubApi.SetAccessList(hubName,
        AccessListFactory.AllowNetworkOnly(network, networkSubnet,
            natOptions.DhcpGatewayAddress, natOptions.DhcpSubnetMask));
}
```

## Activate SecureNat and replace accessList to allow devices only
```c#
var ip = "";
ushort port = 5555; //one of the ports out of the Listener List (443, 992, 1194, 5555)
var pw = "";

using (var softEther = new SoftEther(ip, port))
{
    var connectResult = softEther.Connect();
    var authResult = softEther.Authenticate(pw);

    var network = "192.168.178.0";
    var networkSubnet = "255.255.255.0";
    var hubName = "testHub";
    
    var natOnline = softEther.HubApi.EnableSecureNat(hubName);
    
    //Add route from secureNat to network for clients
    var natOptions = softEther.HubApi.GetSecureNatOptions(hubName);
    var pushRoutesResult = softEther.HubApi.SetSecureNatDhcpPushRoutes(hubName, new DhcpRouteCollection
    {
        {network, networkSubnet, natOptions.DhcpGatewayAddress.ToString()}
    });
    
    //Replace accesslist with DevicesOnly
    var deviceAclList = softEther.HubApi.SetAccessList(hubName, AccessListFactory.AllowDevicesOnly(
                        natOptions.DhcpGatewayAddress, natOptions.DhcpSubnetMask, new AccessDevice("192.168.178.240", "Server")));
    
    //Add another device to the accessList afterwards
    var fritz = softEther.HubApi.AddAccessList(hubName, AccessListFactory.AccessToDevice(AccessListFactory.AllowDevicesPriority, 
        "FritzBox", IPAddress.Parse("192.168.178.1"), natOptions.DhcpGatewayAddress, natOptions.DhcpSubnetMask));
}
```

## Get SoftEther-Certificate
```c#
var ip = "";
ushort port = 5555; //one of the ports out of the Listener List (443, 992, 1194, 5555)
var pw = "";

using (var softEther = new SoftEther(ip, port))
{
    var connectResult = softEther.Connect();
    var authResult = softEther.Authenticate(pw);

    var serverCert = softEther.ServerApi.GetCert();
    Console.WriteLine(serverCert.Cert.Issuer);
}
```
