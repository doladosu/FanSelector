using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FanSelector.Api.Setup.Core;
using FanSelector.Core;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace FanSelector.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="THub"></typeparam>
    public class BaseApiControllerHub<THub> : ApiController where THub : IHub
    {
        Lazy<IHubContext> _hub = new Lazy<IHubContext>(
           () => GlobalHost.ConnectionManager.GetHubContext<THub>()
       );

        /// <summary>
        /// 
        /// </summary>
        protected IHubContext Hub => _hub.Value;

        /// <summary>
        /// 
        /// </summary>
        public string ClientIpAddress => HttpContext.Current.Request.UserHostAddress;

        /// <summary>
        /// IQueryDispatcher for all queries
        /// </summary>
        public readonly IQueryDispatcher QueryDispatcher;

        /// <summary>
        /// ICommandDispatcher for all commands
        /// </summary>
        public readonly ICommandDispatcher CommandDispatcher;

        /// <summary>
        /// BaseApiController declaration, takes IQueryDispatcher and ICommandDispatcher as params
        /// </summary>
        /// <param name="commandDispatcher"></param>
        /// <param name="queryDispatcher"></param>
        public BaseApiControllerHub(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            CommandDispatcher = commandDispatcher;
            QueryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Wraps a controller action in a try/catch and returns the appropriate <c>IHttpActionResult</c>.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <param name="memberParameters"></param>
        /// <returns></returns>
        protected async Task<IHttpActionResult> TryAsync(Func<Task<IHttpActionResult>> func,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            params object[] memberParameters)
        {
            using (new ApiTimedEvent(true, memberName, sourceFilePath, sourceLineNumber, memberParameters))
            {
                Exception e = null;
                try
                {
                    return await func();
                }
                catch (UnauthorizedAccessException uae)
                {
                    e = uae;

                    return StatusCode(HttpStatusCode.Forbidden);
                }
                catch (ArgumentException aex)
                {
                    e = aex;

                    return BadRequest(aex.ParamName ?? aex.Message);
                }
                catch (FaultException f)
                {
                    e = f;

                    if (f.Message.Contains("404"))
                        return NotFound();

                    return BadRequest(f.Message);
                }
                catch (Exception ex)
                {
                    e = ex;

                    return InternalServerError(ex);
                }
                finally
                {

                }
            }
        }
    }
}
