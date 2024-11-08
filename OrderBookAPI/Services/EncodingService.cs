using System.Web;

namespace OrderBookAPI.Services
{
    public class EncodingService
    {
        // URL encode med undtagelse af mellemrum
        public string CustomUrlEncode(string input)
        {
            string encodedInput = HttpUtility.UrlEncodeUnicode(input);
            encodedInput = encodedInput.Replace("%20", " ");  // Erstat %20 med mellemrum
            return encodedInput;
        }

        // HTML encode
        public string HtmlEncode(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        // URL decode
        public string CustomUrlDecode(string input)
        {
            return HttpUtility.UrlDecode(input);
        }

        // HTML decode
        public string HtmlDecode(string input)
        {
            return HttpUtility.HtmlDecode(input);
        }
    }
}
