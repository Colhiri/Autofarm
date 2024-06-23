using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autofarm.Сommon
{
    public class BaseToken
    {
        public int Id { get; set; }
        public string Data { get; set; }

        public int BaseGameInfoId { get; set; }
        public BaseGameInfo? BaseGameInfo { get; set; }
    }
}
