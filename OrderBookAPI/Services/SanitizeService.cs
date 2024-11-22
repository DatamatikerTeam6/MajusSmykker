using Ganss.Xss;

namespace OrderBookAPI.Services
{
    public class SanitizeService
    {
        private readonly HtmlSanitizer _htmlSanitizer;

        public SanitizeService()
        {
            _htmlSanitizer = new HtmlSanitizer();
        }

        // Metode til at rense input og fjerne skadelige tags
        public string SanitizeHtml(string input)
        {
            return _htmlSanitizer.Sanitize(input);
        }
    }
}
