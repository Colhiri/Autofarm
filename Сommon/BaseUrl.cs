using System.ComponentModel.DataAnnotations;

namespace Autofarm.Сommon
{
    public class BaseUrl
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int idHeader { get; set; }
     
        public int BaseGameInfoId { get; set; }
        public BaseGameInfo? BaseGameInfo { get; set; }

        public int BaseHeaderId { get; set; }
        public BaseHeader? BaseHeader { get; set; }

    }
}
