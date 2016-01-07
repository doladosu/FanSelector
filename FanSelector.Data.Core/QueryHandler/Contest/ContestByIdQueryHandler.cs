using AutoMapper;
using FanSelector.Core;
using FanSelector.Data.Core.Query;
using FanSelector.Data.Core.QueryResult.Contest;
using FanSelector.Data.QueryService;
using FanSelector.Models.Output;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanSelector.Data.Core.QueryHandler.Contest
{
    public class ContestByIdQueryHandler : IQueryHandler<BaseByIdQuery, ContestByIdQueryResult>
    {
        private readonly IContestQueryRepository _contestRepository;

        public ContestByIdQueryHandler(IContestQueryRepository contestRepository)
        {
            _contestRepository = contestRepository;
        }

        public async Task<ContestByIdQueryResult> Retrieve(BaseByIdQuery query)
        {
            var allData = await _contestRepository.GetAllContests();
            if (!string.IsNullOrEmpty(query.UserId))
            {
                //Get data from Contest entry table and map to Contests record
            }
            var result = new ContestByIdQueryResult
            {
                Contest = Mapper.Map<Contests>(allData),
            };
            return result;
        }
    }
}