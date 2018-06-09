using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
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
            _socket.AuthenticateAsClient(host, null, SslProtocols.Tls12, false);

            RandomFromServer = null;
            ServerApi = new SoftEtherServer(this);
            HubApi = new SoftEtherHub(this);
        }

        public void Dispose()
        {
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

            if (connectResult.Valid())
                RandomFromServer = connectResult.random;

            return connectResult;
        }

        public AuthResult Authenticate(string password, string hubName = null)
        {
            var passwordHash = CreatePasswordHash(password);
            return Authenticate(passwordHash, hubName);
        }

        public AuthResult Authenticate(byte[] passwordHash, string hubName = null)
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

            var securePassword = CreateSaltedHash(passwordHash, RandomFromServer);
            authPayload.Add("secure_password", securePassword);

            var serializedAuthPayload = SoftEtherProtocol.Serialize(authPayload);
            _socket.SendHttpRequest("POST", "/vpnsvc/vpn.cgi", serializedAuthPayload,
                SoftEtherNetwork.GetDefaultHeaders());

            var authResponse = _socket.GetHttpResponse();

            if (authResponse.code != 200)
                return new AuthResult {Error = SoftEtherError.AuthFailed};

            var authDict = SoftEtherProtocol.Deserialize(authResponse.body);
            return AuthResult.Deserialize(authDict);
        }

        public SoftEtherParameterCollection CallMethod(string functionName,
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
        
        public SoftEtherHashPair CreateUserHashAndNtLm(string name, string password)
        {
            var hashedPw = CreateUserPasswordHash(name, password);
            var ntlmHash = CreateNtlmHash(password);
            return new SoftEtherHashPair(hashedPw, ntlmHash);
        }

        public static byte[] CreateUserPasswordHash(string username, string password)
        {
            var hashCreator = new SHA0();
            hashCreator.Update(Encoding.ASCII.GetBytes(password));
            var hashedPw = hashCreator.Update(Encoding.ASCII.GetBytes(username.ToUpper())).Digest();
            return hashedPw;
        }

        public static byte[] CreateNtlmHash(string password)
        {
            var hashCreator = new MD4();
            var ntmlHash = hashCreator.Update(Encoding.Unicode.GetBytes(password)).Digest();
            return ntmlHash;
        }
        
        public SoftEtherHashPair CreateHashAnSecure(string password)
        {
            var hashedPw = CreatePasswordHash(password);
            var saltedPw = CreateSaltedHash(hashedPw, RandomFromServer);
            
            return new SoftEtherHashPair(hashedPw, saltedPw);
        }

        public static byte[] CreatePasswordHash(string password)
        {
            var hashCreator = new SHA0();
            var hashedPw = hashCreator.Update(Encoding.ASCII.GetBytes(password)).Digest();
            
            return hashedPw;
        }
        
        public static byte[] CreateSaltedHash(byte[] passwordHash, byte[] salt)
        {
            var hashCreator = new SHA0();
            hashCreator.Update(passwordHash);
            var saltedPw = hashCreator.Update(salt).Digest();
            
            return saltedPw;
        }
    }
}