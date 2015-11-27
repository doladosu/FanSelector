using System.Threading.Tasks;

namespace FanSelector.Core
{
    /// <summary>
    /// Passed around to all allow dispatching a command and to be mocked by unit tests
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatches a command to its handler
        /// </summary>
        /// <typeparam name="TParameter">Command Type</typeparam>
        /// <param name="command">The command to be passed to the handler</param>
        Task<CommandResult> Dispatch<TParameter>(TParameter command) where TParameter : ICommand;
    }
}