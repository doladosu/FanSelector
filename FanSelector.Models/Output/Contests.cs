using System;

namespace FanSelector.Models.Output
{
    public class Contests
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long Prizes { get; set; }

        public int EntryFee { get; set; }

        public int Size { get; set; }

        public int Entries { get; set; }

        public DateTime ContestDate { get; set; }
    }
}