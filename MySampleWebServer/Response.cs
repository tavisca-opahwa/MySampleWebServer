using System;
using System.Collections.Generic;
using System.IO;

namespace MySampleWebServer
{

    public class FileHandler
    {

    }
    public class Response
    {
        private Byte[] _data = null;
        private string _status;
        private string _mime;
      
        public Response(String status, string mime, Byte[] data)
        {
            _data = data;
            _status = status;
            _mime = mime;
        }

        public static Response From(Request request)
        {
            if (RequestValidator.IsValidRequest(request) == false)
                return ErrorResponseHandler.GetErrorResponse(request);

            if (request.Type == "GET")
            {
                String file = Environment.CurrentDirectory + LocationConstants.WEB_DIR + request.URL;
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
                        return ErrorResponseHandler.GetErrorResponse(request);
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
                    return ErrorResponseHandler.GetErrorResponse(request);
            }
            else
            {
                return ErrorResponseHandler.GetErrorResponse(request);
            }
            return ErrorResponseHandler.GetErrorResponse(request);
        }


        private static Response MakeFromFile(FileInfo f,string mineType)
        {
            FileStream fs = f.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            //if (MineType.SupportedMine.ContainsKey(mineType) == false)
            //    return ErrorResponseHandler.GetErrorResponse();
            return new Response("200 OK", MineType.SupportedMine[mineType] , d);
        }


        public void Post(Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(string.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n", LocationConstants.VERSION, _status, LocationConstants.NAME, _mime, _data.Length));
            writer.Flush();
            stream.Write(_data, 0, _data.Length);
        }
    }
}
