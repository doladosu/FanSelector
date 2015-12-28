using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FanSelector.Models.Output;

namespace FanSelector.Data.QueryService.Impl
{
    public class ListQueryRepository : BaseRepository, IListQueryRepository
    {
        public Task<IEnumerable<State>> GetAllStates()
        {
            throw new NotImplementedException();
            //var allStates = await Db.States.ToListAsync();
            //return Mapper.Map<IEnumerable<State>>(allStates);
        }
    }
}