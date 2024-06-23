using System.Reflection.Metadata;
using System.Text.Json;

namespace Autofarm.FuelMining.Responses
{
    public class PlayGameResponse
    {
        private UserMining _userMining;
        public object userMining 
        {
            get { return _userMining; }
            set 
            {
                JsonElement jsonElement = (JsonElement)value;

                string jsonValue = jsonElement.GetRawText();

                _userMining = JsonSerializer.Deserialize<UserMining>(jsonValue);
            }
        }
    }

    public class UserMining
    {
        public long userId { get; set; }
        public decimal? speed { get; set; }
        public long? dttmLastClaim { get; set; }
        public decimal? miningAmount { get; set; }
        public int? status { get; set; }
        public decimal? gotAmount { get; set; }
        public int? sentNotificationAmount { get; set; }
        public int? guild { get; set; }
        public string? alliance { get; set; }
        public long? dttmClaimDeadline { get; set; }
        public long? dttmLastPayment { get; set; }
    }
}