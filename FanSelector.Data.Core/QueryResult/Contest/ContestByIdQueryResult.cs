using FanSelector.Models.Output;

namespace FanSelector.Data.Core.QueryResult.Contest
{
    public class ContestByIdQueryResult : BaseQueryResult
    {
        public Contests Contest { get; set; }
    }
}
