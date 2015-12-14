using System.Web.Http;
using System.Web.Http.Filters;
using FanSelector.Api.Setup;
using FanSelector.Api.Setup.Core;
using Microsoft.Owin.Security.OAuth;

namespace FanSelector.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            RegisterFilters(config.Filters);

            // Web API configuration and services
            RegisterModelBinders();

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void RegisterModelBinders()
        {
            // http://stackoverflow.com/questions/21180321/webapi-actions-with-multiple-complex-types-one-of-which-is-injected-with-a-filt
            GlobalConfiguration.Configuration.BindParameter(typeof(ILoggedInPerson), new NoOpModelBinder());
        }

        private static void RegisterFilters(HttpFilterCollection filters)
        {
            filters.Add(new PersonActionParameterAttribute());
        }

    }
}
