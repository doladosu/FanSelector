using FanSelector.Core;
using FanSelector.Models.Db;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanSelector.Data.QueryService
{
    public interface IContestQueryRepository : IRepository
    {
        Task<IEnumerable<Contest>> GetAllContests();
        Task<Contest> GetContestById(int id);
    }
}