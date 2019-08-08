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
        public static Request GetRequest(RequestProperties request)
        {
            return new Request(request);
        }
        public String Type { get; set; }
        public String URL { get; set; }
        public String Host { get; set; }

    }
}
