using System.Collections.Generic;

namespace FanSelector.Models.Input
{
    public class ContestEntry
    {
        public int Id { get; set; }
        public IEnumerable<Player> Players { get; set; } 
    }
}