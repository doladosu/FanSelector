using System;
using System.Threading.Tasks;
using Ninject;

namespace FanSelector.Core
{
    /// <summary>
    /// Implementation of the query dispatcher - selects and executes the appropriate query
    /// </summary>
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IKernel _kernel;

        public QueryDispatcher(IKernel kernel)
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
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TResult> Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = _kernel.Get<IQueryHandler<TParameter, TResult>>();
            return await handler.Retrieve(query);
        }
    }
}