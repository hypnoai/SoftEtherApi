using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SoftEtherApi.Api;
using SoftEtherApi.Containers;
using SoftEtherApi.Infrastructure;
using SoftEtherApi.SoftEtherModel;

namespace SoftEtherApi
{
    public class SoftEther : IDisposable
    {
        private readonly TcpClient _rawSocket;
        private readonly SslStream _socket;

        public byte[] RandomFromServer { get; private set; }

        public SoftEtherServer ServerApi { get; }
        public SoftEtherHub HubApi { get; }

        public SoftEther(string host, ushort port)
        {
            _rawSocket = new TcpClient(host, port);
            _socket = new SslStream(_rawSocket.GetStream(), false, (sender, certificate, chain, errors) => true, null);
            _socket.AuthenticateAsClient(host);

            RandomFromServer = null;
            ServerApi = new SoftEtherServer(this);
            HubApi = new SoftEtherHub(this);
        }

        public void Dispose()
        {
            _rawSocket.Dispose();
            _socket.Dispose();
        }

        public ConnectResult Connect()
        {
            _socket.SendHttpRequest("POST", "/vpnsvc/connect.cgi", Encoding.ASCII.GetBytes("VPNCONNECT"),
                SoftEtherNetwork.GetDefaultHeaders());

            var connectResponse = _socket.GetHttpResponse();
            if (connectResponse.code != 200)
                return new ConnectResult {Error = SoftEtherError.ConnectFailed};

            var connectDict = SoftEtherProtocol.Deserialize(connectResponse.body);
            var connectResult = ConnectResult.Deserialize(connectDict);

            if (connectDict.ContainsKey("random"))
                RandomFromServer = connectResult.random;

            return connectResult;
        }

        public AuthResult Authenticate(string password, string hubName = null)
        {
            if (RandomFromServer == null)
                return new AuthResult {Error = SoftEtherError.ConnectFailed};

            var authPayload = new SoftEtherParameterCollection
            {
                {"method", "admin"},
                {"client_str", "SoftEtherNet"},
                {"client_ver", 1},
                {"client_build", 0}
            };

            if (!string.IsNullOrWhiteSpace(hubName))
                authPayload.Add("hubname", hubName);

            var (hashedPw, securePw) = CreateHashAnSecure(password);

            authPayload.Add("secure_password", securePw);

            var serializedAuthPayload = SoftEtherProtocol.Serialize(authPayload);
            _socket.SendHttpRequest("POST", "/vpnsvc/vpn.cgi", serializedAuthPayload,
                SoftEtherNetwork.GetDefaultHeaders());

            var authResponse = _socket.GetHttpResponse();

            if (authResponse.code != 200)
                return new AuthResult {Error = SoftEtherError.AuthFailed};

            var authDict = SoftEtherProtocol.Deserialize(authResponse.body);
            return AuthResult.Deserialize(authDict);
        }

        public Dictionary<string, List<object>> CallMethod(string functionName,
            SoftEtherParameterCollection payload = null)
        {
            if (payload == null)
                payload = new SoftEtherParameterCollection();
            
            payload.RemoveNullParameters();
            payload.Add("function_name", functionName);

            var serializedPayload = SoftEtherProtocol.Serialize(payload);
            var serializedLength = SoftEtherProtocol.SerializeInt(serializedPayload.Length);

            _socket.Write(serializedLength);
            _socket.Write(serializedPayload);

            _socket.Flush();

            var dataLength = new byte[4];
            var bytesRead = _socket.Read(dataLength, 0, 4);

            if (bytesRead != 4)
                throw new Exception("Failed to read dataLength");

            var dataLengthAsInt = SoftEtherProtocol.DeserializeInt(dataLength);
            var responseBuffer = new byte[dataLengthAsInt];

            bytesRead = 0;
            for (var i = 0; i < 10; i++) //retrie 10 times to read all data
            {
                bytesRead += _socket.Read(responseBuffer, bytesRead, dataLengthAsInt - bytesRead);
                if (bytesRead == dataLengthAsInt)
                    break;
                Thread.Sleep(50);
            }

            if (bytesRead != dataLengthAsInt)
                throw new Exception("read less than dataLength");

            var response = SoftEtherProtocol.Deserialize(responseBuffer);

            return response;
        }
        
        public (byte[], byte[]) CreateHashAndNtLm(string name, string password)
        {
            var hashedPwCreator = new SHA0();
            hashedPwCreator.Update(Encoding.ASCII.GetBytes(password));
            var hashedPw = hashedPwCreator.Update(Encoding.ASCII.GetBytes(name.ToUpper())).Digest();

            var securePwCreator = new MD4();
            var securePw = securePwCreator.Update(Encoding.Unicode.GetBytes(password)).Digest();
            return (hashedPw, securePw);
        }
        
        public (byte[], byte[]) CreateHashAnSecure(string password)
        {
            var hashedPwCreator = new SHA0();
            var hashedPw = hashedPwCreator.Update(Encoding.ASCII.GetBytes(password)).Digest();

            var securePwCreator = new SHA0();
            securePwCreator.Update(hashedPw);
            var securePw = securePwCreator.Update(RandomFromServer).Digest();
            
            return (hashedPw, securePw);
        }
    }
}