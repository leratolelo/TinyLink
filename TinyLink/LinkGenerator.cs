
namespace TinyLink
{
    public static class LinkGenerator
    {
        public static string _baseUrl = "https://tinylink.com/";

        public static string GenerateShortLink(string originalUrl , string hash)
        {
            return _baseUrl +  hash;
        }

        public static void SetBaseURL(string baseURL)
        { 
            _baseUrl = baseURL;
        }
    }
}
