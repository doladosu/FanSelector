using FanSelector.Core;

namespace FanSelector.Data.Core.QueryResult
{
    public class BaseQueryResult : IQueryResult
    {
        public int TotalRecords { get; set; }
    }
}