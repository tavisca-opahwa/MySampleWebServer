namespace MySampleWebServer
{
    public class RequestValidator
    {
        public static bool IsValidRequest(Request request)
        {
            if (request != null && request.Type == "GET") 
                return true;
             return false;
        }
    }
}
