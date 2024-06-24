using System;
namespace Autofarm.FuelMining.Responses
{
    public class PlayGameResponse
    {
        public string banned_until_restore { get; set; }
        public int boxes_amount { get; set; }
        public int drops_amount { get; set; }
        public int energy { get; set; }
        public DateTime last_energy_update { get; set; }
        public int mined_count { get; set; }
        public int[] mystery_ids { get; set; }
    }
}
