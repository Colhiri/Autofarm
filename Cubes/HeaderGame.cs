using Autofarm.Сommon;
using System.Collections;
using System.Text.Json.Serialization;

namespace Autofarm.Cubes
{
    public class HeaderGame : BaseHeader
    {
        [JsonPropertyName("Accept")]
        public string? Accept { get; set; }
        [JsonPropertyName("Accept-Encoding")]
        public string? Accept_Encoding { get; set; }
        [JsonPropertyName("Accept-Language")]
        public string? Accept_Language { get; set; }
        [JsonPropertyName("User-Agent")]
        public string? User_Agent { get; set; }

        private Dictionary<string, string> headersData = null;

        public IEnumerator GetEnumerator()
        {
            if (headersData == null)
            {
                headersData = new Dictionary<string, string>() {
                    {"Accept", Accept },
                    {"Accept-Encoding", Accept_Encoding },
                    {"Accept-Language", Accept_Language },
                    {"User-Agent", User_Agent }
                    };
            }

            return headersData.GetEnumerator();
        }
    }
}
