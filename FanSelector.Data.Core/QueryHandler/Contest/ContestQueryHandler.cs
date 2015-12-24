using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FanSelector.Core;
using FanSelector.Data.Core.Query;
using FanSelector.Data.Core.QueryResult.Contest;
using FanSelector.Data.QueryService;
using FanSelector.Models.Output;

namespace FanSelector.Data.Core.QueryHandler.Contest
{
    public class ContestQueryHandler : IQueryHandler<BaseByIdQuery, ContestQueryResult>
    {
        private readonly IContestRepository _contestRepository;

        public ContestQueryHandler(IContestRepository contestRepository)
        {
            _contestRepository = contestRepository;
        }

        public async Task<ContestQueryResult> Retrieve(BaseByIdQuery query)
        {
            var allData = await _contestRepository.GetAllContests();
            var result = new ContestQueryResult
            {
                Contests = Mapper.Map<IEnumerable<Contests>>(allData),
                TotalRecords = allData.Count()
            };
            return result;
        }
    }
}