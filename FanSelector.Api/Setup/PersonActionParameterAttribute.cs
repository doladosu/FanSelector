using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;
using FanSelector.Api.Setup.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace FanSelector.Api.Setup
{
    /// <summary>
    /// Logged In person attribute, used in every api action parameter
    /// </summary>
    public class PersonActionParameterAttribute : FilterAttribute, IActionFilter
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private async Task CreateCurrentUserParameter(HttpActionContext actionContext)
        {
            var controller = (ApiController)actionContext.ControllerContext.Controller;
            var user = controller.User;

            if (!user.Identity.IsAuthenticated)
                return;

            var parameter = actionContext.ActionDescriptor.GetParameters().SingleOrDefault(p => typeof(ILoggedInPerson).IsAssignableFrom(p.ParameterType));
            if (parameter == null)
                return;
            LoggedInPerson loggedInPerson = null;
            if (!string.IsNullOrWhiteSpace(user.Identity.Name))
            {
                IdentityUser loggedInUser = await UserManager.FindByIdAsync(user.Identity.GetUserId());


                if (loggedInUser != null)
                {
                    var roles = loggedInUser.Roles;

                    var roleValues =
                        roles.Select(identityUserRole => int.Parse(identityUserRole.RoleId))
                            .Select(enumValue => enumValue.ToString())
                            .ToList();

                    loggedInPerson = new LoggedInPerson(loggedInUser.Id, loggedInUser.UserName, roleValues);
                }
            }

            var currentUser = loggedInPerson;

            actionContext.ActionArguments[parameter.ParameterName] = currentUser;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            await InternalActionExecuting(actionContext, cancellationToken);
            if (actionContext.Response != null)
            {
                return actionContext.Response;
            }

            HttpActionExecutedContext executedContext;

            try
            {
                var response = await continuation();
                executedContext = new HttpActionExecutedContext(actionContext, null)
                {
                    Response = response
                };
            }
            catch (Exception exception)
            {
                executedContext = new HttpActionExecutedContext(actionContext, exception);
            }
            await InternalActionExecuted(executedContext, cancellationToken);
            return executedContext.Response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task InternalActionExecuting(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            await CreateCurrentUserParameter(actionContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task InternalActionExecuted(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Run(() => { }, cancellationToken);
        }
    }
}