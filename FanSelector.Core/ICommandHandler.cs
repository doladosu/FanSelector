using System.Threading.Tasks;

namespace FanSelector.Core
{
    /// <summary>
    /// Base interface for command handlers
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public interface ICommandHandler<in TParameter> where TParameter : ICommand
    {
        /// <summary>
        /// Interface for command handlers - has a type parameters for the command
        /// </summary>
        /// <param name="command">The command to be used</param>
        Task<CommandResult> Execute(TParameter command);
    }
}