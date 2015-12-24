using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using FanSelector.Models.Db;

namespace FanSelector.Data.QueryService.Impl
{
    public class ContestRepository : BaseRepository, IContestRepository
    {
        public async Task<IEnumerable<Contest>> GetAllContests()
        {
            return await Db.Contests.ToListAsync();
        }
    }
}