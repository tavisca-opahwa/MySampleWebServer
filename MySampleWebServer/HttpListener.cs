using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace MySampleWebServer
{
    public interface ITcpClient
    {
        NetworkStream GetStream();
        TcpClient GetClient();
    }
    public class TcpClientAdapter : ITcpClient
    {
        private TcpClient _wrappedClient;
        public TcpClientAdapter(TcpClient client)
        {
            _wrappedClient = client;
        }
        public TcpClient GetClient()
        {
            return _wrappedClient;
        }

        public NetworkStream GetStream()
        {
            return _wrappedClient.GetStream();
        }
    }
    public class HTTPListener
    {
        public static string GetMessageFromClient(TcpClient client)
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
            ITcpClient tca = new TcpClientAdapter(client);
            string msg = GetMessageFromClient(tca.GetClient());
            var requestProperties = HTTPParser.ParseRequest(msg);
            Request req = Request.GetRequest(requestProperties);
            Response resp = Response.From(req);
            resp.Post(client.GetStream());
        }


    }
}
