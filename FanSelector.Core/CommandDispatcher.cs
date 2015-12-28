using System;
using System.Threading.Tasks;
using Ninject;

namespace FanSelector.Core
{
    /// <summary>
    /// Implementation of the command dispatcher - selects and executes the appropriate command
    /// </summary>
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IKernel _kernel;

        public CommandDispatcher(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _kernel = kernel;
        }

        /// <summary>
        /// Find the appropriate handler to call from those registered with Ninject based on the type parameters  
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<CommandResult> Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            var handler = _kernel.Get<ICommandHandler<TParameter>>();
            return await handler.Execute(command);
        }
    }
}