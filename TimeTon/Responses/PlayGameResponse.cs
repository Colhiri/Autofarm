using System.Text.Json;

namespace Autofarm.TimeTon.Responses
{
    public class PlayGameResponse
    {
        private Message _message;
        public object message
        {
            get { return _message; }
            set
            {
                JsonElement jsonElement = (JsonElement)value;

                string jsonValue = jsonElement.GetRawText();

                _message = JsonSerializer.Deserialize<Message>(jsonValue);
            }
        }

        public int code { get; set; }
    }

    public class Message
    {
        
        private MiningData _miningData;
        public object miningData
        {
            get { return _miningData; }
            set
            {
                JsonElement jsonElement = (JsonElement)value;

                string jsonValue = jsonElement.GetRawText();

                _miningData = JsonSerializer.Deserialize<MiningData>(jsonValue);
            }
        }

        private UserData _userData;
        public object userData
        {
            get { return _userData; }
            set
            {
                JsonElement jsonElement = (JsonElement)value;

                string jsonValue = jsonElement.GetRawText();

                _userData = JsonSerializer.Deserialize<UserData>(jsonValue);
            }
        }
    }

    public class MiningData
    {
        public long lastClaimTime { get; set; }
        public decimal miningRate { get; set; }
        public long crackTime { get; set; }
    }

    public class UserData
    {
        public long balance  { get; set; }
        public long tokens { get; set; }
    }
}