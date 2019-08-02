using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace MySampleWebServer
{
    public class HTTPListener
    {
        private static string GetMessageFromClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());
            String msg = "";
            while (reader.Peek() != -1)
            {
                msg += reader.ReadLine() + "\n";
            }
            Debug.WriteLine("Request: \n" + msg);
            return msg;
        }
        public static void ListenClient(TcpListener listener)
        {
            Console.WriteLine("Waiting for connection....");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected");
            HandleClient(client);
            client.Close();
        }
        private static void HandleClient(TcpClient client)
        {
            string msg = GetMessageFromClient(client);
            var requestProperties = HTTPParser.ParseRequest(msg);
            Request req = Request.GetRequest(requestProperties);
            Response resp = Response.From(req);
            resp.Post(client.GetStream());
        }


    }
}
