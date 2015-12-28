using System.Collections.Generic;
using FanSelector.Models.Output;

namespace FanSelector.Data.Core.QueryResult.List
{
    public class StateQueryResult : BaseQueryResult
    {
        public IEnumerable<State> States { get; set; }
    }
}