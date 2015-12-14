using System.Collections.Generic;
using FanSelector.Models.Output;

namespace FanSelector.Data.Core.QueryResult.Contest
{
    public class ContestQueryResult : BaseQueryResult
    {
        public IEnumerable<Contests> Contests { get; set; }
    }
}
