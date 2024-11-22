using System.Text.Unicode;
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
            encodedInput = encodedInput.Replace("&#230", "æ");
            encodedInput = encodedInput.Replace("%u00e6", "æ");
            encodedInput = encodedInput.Replace("%u00c5", "å");
            encodedInput = encodedInput.Replace("%u00e5", "å");
            encodedInput = encodedInput.Replace("+", " ");
            encodedInput = encodedInput.Replace("%u00f8", "ø");
            encodedInput = encodedInput.Replace("%u00d8", "Ø");
            encodedInput = encodedInput.Replace("%2c", ",");
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
