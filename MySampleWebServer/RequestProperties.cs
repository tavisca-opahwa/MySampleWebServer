namespace MySampleWebServer
{
    public class RequestProperties
    {
        public string Type { get; private set; }
        public string Url { get; private set; }
        public string Host { get; private set; }
        public string Referer { get; private set; }

        public RequestProperties(string type, string url, string host, string referer)
        {
            Type = type;
            Url = url;
            Host = host;
            Referer = referer;
        }
    }
}
