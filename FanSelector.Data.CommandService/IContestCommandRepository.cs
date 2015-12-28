using System.Threading.Tasks;
using FanSelector.Core;

namespace FanSelector.Data.CommandService
{
    public interface IContestCommandRepository : IRepository
    {
        Task EnterContestTask();
    }
}