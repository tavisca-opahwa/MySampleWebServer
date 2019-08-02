using System;
using System.IO;

namespace MySampleWebServer
{
    public class ErrorResponseHandler
    {

        internal static Response GetErrorResponse(Request request)
        {
            if (request == null)
                return MakeNullRequest();
            if (request.Type != "GET")
                return MakeMethodNotAllowed();
            return MakePageNotFound();
        }

        private static Response MakeMethodNotAllowed()
        {
            byte[] d = GetErrorMessage("405.html");
            return new Response("405 Method Not Allowed", "text/html", d);
        }
        private static Response MakePageNotFound()
        {
            byte[] d = GetErrorMessage("404.html");
            return new Response("404 Page Not Found", "text/html", d);
        }
        private static Response MakeNullRequest()
        {
            byte[] d = GetErrorMessage("400.html");
            return new Response("400 Bad Request", "text/html", d);
        }
        private static byte[] GetErrorMessage(string fileName)
        {
            String file = Environment.CurrentDirectory + LocationConstants.MSG_DIR + fileName;
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            return d;
        }


    }
}
