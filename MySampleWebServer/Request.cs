using System;

namespace MySampleWebServer
{
    public class Request
    {
        public Request(RequestProperties requestProperties)
        {
            Type = requestProperties.Type;
            URL = requestProperties.Url;
            Host = requestProperties.Host;
        }

        public static Request GetRequest(String request)
        {
            var requestProperties = HTTPParser.ParseRequest(request);
            return new Request(requestProperties);
        }

        public String Type { get; set; }
        public String URL { get; set; }
        public String Host { get; set; }

    }
    public class HTTPParser
    {
        public static RequestProperties ParseRequest(string request)
        {
            if (String.IsNullOrEmpty(request))
                return null;
            String[] tokens = request.Split(' ');
            String type = tokens[0];
            String url = tokens[1];
            String host = tokens[4];
            String referer = "";
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == "Referer:")
                {
                    referer = tokens[i + 1];
                    break;
                }
            }
            return new RequestProperties(type, url, host, referer);
        }
    }
}
