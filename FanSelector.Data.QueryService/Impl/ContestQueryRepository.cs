using FanSelector.Models.Db;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FanSelector.Data.QueryService.Impl
{
    public class ContestQueryRepository : BaseRepository, IContestQueryRepository
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

        public async Task<Contest> GetContestById(int id)
        {
            var contest = await RedisRepository.Get<Contest>(ContestsKey + id);
            if (contest != null) return contest;
            contest = await Db.Contests.Where(c => c.Id == id).FirstOrDefaultAsync();
            await RedisRepository.Add(ContestsKey + id, contest);
            return contest;
        }
    }
}