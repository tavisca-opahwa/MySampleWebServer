using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MySampleWebServer
{
    public class HTTPServer
    {
        private bool running = false;
        private TcpListener listener;
        public HTTPServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }
        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }
        private void Run()
        {
            running = true;
            listener.Start();
            while (running)
            {
                HTTPListener httpListener = new HTTPListener();
                HTTPListener.ListenClient(listener);
            }
            running = false;
            listener.Stop();
        }

              
    }
}
