using System.ComponentModel.DataAnnotations;

namespace Autofarm.Сommon
{
    public class BaseGameInfo
    {
        public int Id { get; set; }
        public string NameGame { get; set; }

        public List<BaseUrl> urls { get; set; } = new();
        public List<BaseToken> tokens { get; set; } = new();

    }
}
