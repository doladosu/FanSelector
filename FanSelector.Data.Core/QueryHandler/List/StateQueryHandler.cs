using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Data.Core.Query;
using FanSelector.Data.Core.QueryResult.List;
using FanSelector.Data.QueryService;

namespace FanSelector.Data.Core.QueryHandler.List
{
    public class StateQueryHandler : IQueryHandler<BaseByIdQuery, StateQueryResult>
    {
        private readonly IListQueryRepository _listQueryRepository;

        public StateQueryHandler(IListQueryRepository listQueryRepository)
        {
            _listQueryRepository = listQueryRepository;
        }

        public async Task<StateQueryResult> Retrieve(BaseByIdQuery query)
        {
            var states = await _listQueryRepository.GetAllStates();

            return new StateQueryResult
            {
                States = states
            };
        }
    }
}