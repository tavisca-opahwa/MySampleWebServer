using System;
using System.Collections.Generic;
using System.Text;

namespace MySampleWebServer
{
    public class MineType
    {
        public static Dictionary<string, string> SupportedMine = new Dictionary<string, string>
        {
            {".html","text/html" },
            {".htm","text/htm" },
            {".jpeg","image/jpeg" },
            {".png" ,"image/png"},
            {".jpg" ,"image/jpg"},
            {".txt" ,"text/txt"},
             {".css","text/css" },
            {".js" ,"text/js"},
            {".mp3" ,"audio/mp3"},
            {"mp4" ,"video/mp4"}

        };
    }
}
