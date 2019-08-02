using System;

namespace MySampleWebServer
{
    public class HTTPParser
    {
        public static RequestProperties ParseRequest(string request)
        {
            if (String.IsNullOrEmpty(request))
                return null;
            String[] tokens = request.Split(' ');
            String type = tokens[0];
            String url = tokens[1];
            String host = tokens[3].Substring(0,tokens[3].IndexOf('\n'));
            String referer = "";
            for (int i = 0; i < tokens.Length; i++)
                if (tokens[i] == "Referer:")
                {
                    referer = tokens[i + 1];
                    break;
                }
            return new RequestProperties(type, url, host, referer);
        }
    }
}
