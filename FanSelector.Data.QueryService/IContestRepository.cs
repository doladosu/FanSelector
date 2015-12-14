using System.Collections.Generic;
using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Models.Db;

namespace FanSelector.Data.QueryService
{
    public interface IContestRepository : IRepository
    {
        Task<IEnumerable<Contests>> GetAllContests();
    }
}