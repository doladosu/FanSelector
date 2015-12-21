using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FanSelector.Models.Db;

namespace FanSelector.Data.QueryService.Impl
{
    public class ContestRepository : IContestRepository
    {
        public Task<IEnumerable<Contests>> GetAllContests()
        {
            throw new NotImplementedException();
        }
    }
}
