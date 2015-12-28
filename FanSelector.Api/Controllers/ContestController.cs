using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using FanSelector.Api.Hubs;
using FanSelector.Api.Setup.Core;
using FanSelector.Core;
using FanSelector.Data.Core.Command.Contest;
using FanSelector.Data.Core.Query;
using FanSelector.Data.Core.QueryResult.Contest;
using FanSelector.Models.Input;
using FanSelector.Models.Output;

namespace FanSelector.Api.Controllers
{
    /// <summary>
    /// Contests API class
    /// </summary>
    [RoutePrefix("api/Contests")]
    [Authorize]
    public class ContestController : BaseApiControllerHub<ContestsHub>
    {
        /// <summary>
        /// Contests API class declaration
        /// </summary>
        /// <param name="commandDispatcher"></param>
        /// <param name="queryDispatcher"></param>
        public ContestController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <summary>
        /// Gets all live contests.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [EnableQuery]
        [ResponseType(typeof(IEnumerable<Contests>))]
        public async Task<IHttpActionResult> GetAllContestsTask(ILoggedInPerson loggedInPerson)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery();
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, ContestQueryResult>(baseByIdQuery);
                return new CustomOkResult<IEnumerable<Contests>>(result.Contests, this)
                {
                    XInlineCount = result.TotalRecords.ToString()
                };
            }, memberParameters: new object[] {loggedInPerson});
        }

        /// <summary>
        /// Gets all contests entered by user
        /// </summary>
        /// <param name="loggedInPerson"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User")]
        [EnableQuery]
        [ResponseType(typeof(IEnumerable<Contests>))]
        public async Task<IHttpActionResult> GetContestsForUserTask(ILoggedInPerson loggedInPerson)
        {
            return await TryAsync(async () =>
            {
                var baseByIdQuery = new BaseByIdQuery { UserId = loggedInPerson.Id };
                var result = await QueryDispatcher.Dispatch<BaseByIdQuery, ContestQueryResult>(baseByIdQuery);
                return new CustomOkResult<IEnumerable<Contests>>(result.Contests, this)
                {
                    XInlineCount = result.TotalRecords.ToString()
                };
            }, memberParameters: new object[] { loggedInPerson });
        }

        /// <summary>
        /// Enter user to a live contest
        /// </summary>
        /// <param name="loggedInPerson"></param>
        /// <param name="contestEntry"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(CommandResult))]
        public async Task<IHttpActionResult> EnterContestTask(ILoggedInPerson loggedInPerson, [FromBody]ContestEntry contestEntry)
        {
            return await TryAsync(async () =>
            {
                var command = new EnterContestCommand { ContestEntry = contestEntry, UserId = loggedInPerson.Id };
                var result = await CommandDispatcher.Dispatch(command);
                Hub.Clients.All.enteredContest(contestEntry);
                return Ok(result);
            }, memberParameters: new object[] { loggedInPerson, contestEntry });
        }

        /// <summary>
        /// Update user contest entry
        /// </summary>
        /// <param name="loggedInPerson"></param>
        /// <param name="contestEntry"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(CommandResult))]
        public async Task<IHttpActionResult> UpdateContestTask(ILoggedInPerson loggedInPerson, [FromBody]ContestEntry contestEntry)
        {
            return await TryAsync(async () =>
            {
                var command = new EnterContestCommand { ContestEntry = contestEntry, UserId = loggedInPerson.Id };
                var result = await CommandDispatcher.Dispatch(command);
                return Ok(result);
            }, memberParameters: new object[] { loggedInPerson, contestEntry });
        }
    }
}