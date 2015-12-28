using System.Collections.Generic;
using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Models.Output;

namespace FanSelector.Data.QueryService
{
    public interface IListQueryRepository : IRepository
    {
        Task<IEnumerable<State>> GetAllStates();
    }
}