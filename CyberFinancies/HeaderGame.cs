using Autofarm.Сommon;
using System.Collections;
using System.Text.Json.Serialization;

namespace Autofarm.CyberFinancies
{
    public class HeaderGame : BaseHeader
    {
        [JsonPropertyName("Accept")]
        public string? Accept { get; set; }
        [JsonPropertyName("Accept-Encoding")]
        public string? Accept_Encoding { get; set; }
        [JsonPropertyName("Accept-Language")]
        public string? Accept_Language { get; set; }
        [JsonPropertyName("If-None-Match")]
        public string? If_None_Match { get; set; }
        [JsonPropertyName("Origin")]
        public string? Origin { get; set; }
        [JsonPropertyName("Priority")]
        public string? Priority { get; set; }
        [JsonPropertyName("Referer")]
        public string? Referer { get; set; }
        [JsonPropertyName("Sec-Ch-Ua")]
        public string? Sec_Ch_Ua { get; set; }
        [JsonPropertyName("Sec-Ch-Ua-Mobile")]
        public string? Sec_Ch_Ua_Mobile { get; set; }
        [JsonPropertyName("Sec-Ch-Ua-Platform")]
        public string? Sec_Ch_Ua_Platform { get; set; }
        [JsonPropertyName("Sec-Fetch-Dest")]
        public string? Sec_Fetch_Dest { get; set; }
        [JsonPropertyName("Sec-Fetch-Mode")]
        public string? Sec_Fetch_Mode { get; set; }
        [JsonPropertyName("Sec-Fetch-Site")]
        public string? Sec_Fetch_Site { get; set; }
        [JsonPropertyName("Secret-Key")]
        public string? Secret_Key { get; set; }
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
                    {"If_None_Match", If_None_Match },
                    {"Origin", Origin },
                    {"Priority", Priority },
                    {"Referer", Referer },
                    {"Sec_Ch_Ua", Sec_Ch_Ua },
                    {"Sec_Ch_Ua_Mobile", Sec_Ch_Ua_Mobile },
                    {"Sec_Ch_Ua_Platform", Sec_Ch_Ua_Platform },
                    {"Sec_Fetch_Dest", Sec_Fetch_Dest },
                    {"Sec_Fetch_Mode", Sec_Fetch_Mode },
                    {"Sec_Fetch_Site", Sec_Fetch_Site },
                    {"Secret_Key", Secret_Key },
                    {"User-Agent", User_Agent }
                    };
            }

            return headersData.GetEnumerator();
        }
    }
}
