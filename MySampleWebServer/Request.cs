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
}
