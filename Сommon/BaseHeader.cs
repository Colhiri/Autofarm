using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autofarm.Сommon
{
    public class BaseHeader
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}
