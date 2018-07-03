using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Text;
using SoftEtherApi.Extensions;
using SoftEtherApi.Model;

namespace SoftEtherApi.Containers
{
    public static class SoftEtherNetwork
    {
        public static Dictionary<string, string> GetDefaultHeaders()
        {
            return new Dictionary<string, string>
            {
                {"Keep-Alive", "timeout=15; max=19"},
                {"Connection", "Keep-Alive"},
                {"Content-Type", "application/octet-stream"}
            };
        }

        public static void SendHttpRequest(this SslStream socket, string method, string target, byte[] body,
            Dictionary<string, string> headers)
        {
            var header = $"{method.ToUpper()} {target} HTTP/1.1\r\n";

            foreach (var el in headers) 
                header += $"{el.Key}: {el.Value}\r\n";

            if (!headers.ContainsKey("Content-Length"))
                header += $"Content-Length: {body.Length}\r\n";

            socket.Write(Encoding.ASCII.GetBytes($"{header}\r\n"));
            socket.Write(body);

            socket.Flush();
        }

        public static SoftEtherHttpResult GetHttpResponse(this SslStream socket)
        {
            var stream = new BinaryReader(socket);

            var firstLine = stream.ReadLine();
            var responseCode = Convert.ToInt32(firstLine.Substring(9, 3));
            var responseHeaders = new Dictionary<string, string>();
            var responseLength = 0;

            while (true)
            {
                var headerLine = stream.ReadLine();

                if (string.IsNullOrEmpty(headerLine))
                    break;

                var (headerName, headerValue, _) = headerLine.Split(':').Select(m => m.Trim()).ToArray();
                headerName = headerName.ToLower();
                responseHeaders.Add(headerName, headerValue);

                if (headerName == "content-length")
                    responseLength = int.Parse(headerValue);
            }

            var responseBody = new byte[responseLength];
            var bytesRead = 0;
            while (bytesRead < responseLength)
                bytesRead += stream.Read(responseBody, bytesRead, responseLength - bytesRead);


            return new SoftEtherHttpResult
            {
                code = responseCode,
                headers = responseHeaders,
                length = responseLength,
                body = responseBody
            };
        }
    }
}