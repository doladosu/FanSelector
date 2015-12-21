using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using FanSelector.Api.Setup.Core;
using FanSelector.Core;
using FanSelector.Data.Core.Query;
using FanSelector.Data.Core.QueryResult.Contest;
using FanSelector.Models.Output;

namespace FanSelector.Api.Controllers
{
    [RoutePrefix("api/Contests")]
    [Authorize]
    public class ContestController : BaseApiController
    {
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
    }
}
