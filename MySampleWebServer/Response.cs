using System;
using System.Collections.Generic;
using System.IO;

namespace MySampleWebServer
{
    public class Response
    {
        private Byte[] _data = null;
        private string _status;
        private string _mime;
      
        private Response(String status, string mime, Byte[] data)
        {
            _data = data;
            _status = status;
            _mime = mime;
        }

        public static Response From(Request request)
        {
            if (request == null)
                return MakeNullRequest();

            if (request.Type == "GET")
            {
                String file = Environment.CurrentDirectory + HTTPServer.WEB_DIR + request.URL;
                FileInfo f = new FileInfo(file);
                if (f.Exists && f.Extension.Contains("."))
                {
                    string extesion = Path.GetExtension(file);
                    return MakeFromFile(f,extesion);
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(f + "/");
                    if (!di.Exists)
                        return MakePageNotFound();
                    FileInfo[] files = di.GetFiles();
                    foreach (FileInfo ff in files)
                    {
                        string n = ff.Name;
                        if (n.Contains("index.html") || n.Contains("index.htm") || n.Contains("default.html") || n.Contains("default.htm"))
                        {
                            MakeFromFile(ff,".html");
                        }
                    }
                }
                if (!f.Exists)
                    return MakePageNotFound();
            }
            else
            {
                return MakeMethodNotAllowed();
            }
            return MakePageNotFound();
        }

        private static Response MakeMethodNotAllowed()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "405.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            return new Response("405 Method Not Allowed", "text/html", d);
        }

        private static Response MakePageNotFound()
        {

            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "404.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            return new Response("404 Page Not Found", "text/html", d);
        }

        private static Response MakeFromFile(FileInfo f,string mineType)
        {
            FileStream fs = f.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            if (MineType.SupportedMine.ContainsKey(mineType) == false)
                return MakeNullRequest();
            return new Response("200 OK", MineType.SupportedMine[mineType] , d);
        }

        private static Response MakeNullRequest()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "400.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            return new Response("400 Bad Request", "text/html", d);
        }

        public void Post(Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(string.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n", HTTPServer.VERSION, _status, HTTPServer.NAME, _mime, _data.Length));
            writer.Flush();
            stream.Write(_data, 0, _data.Length);
        }
    }
}
