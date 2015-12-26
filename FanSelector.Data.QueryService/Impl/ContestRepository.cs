using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using FanSelector.Models.Db;

namespace FanSelector.Data.QueryService.Impl
{
    public class ContestRepository : BaseRepository, IContestRepository
    {
        private const string ContestsKey = "contests";

        public async Task<IEnumerable<Contest>> GetAllContests()
        {
            var contests = await RedisRepository.Get<IEnumerable<Contest>>(ContestsKey);
            if (contests != null) return contests;
            contests = await Db.Contests.ToListAsync();
            await RedisRepository.Add(ContestsKey, contests);
            return contests;
        }
    }
}