using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace FanSelector.Api.Setup.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomOkResult<T> : OkNegotiatedContentResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="controller"></param>
        public CustomOkResult(T content, ApiController controller)
            : base(content, controller) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentNegotiator"></param>
        /// <param name="request"></param>
        /// <param name="formatters"></param>
        public CustomOkResult(T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
            : base(content, contentNegotiator, request, formatters) { }

        /// <summary>
        /// 
        /// </summary>
        public string XInlineCount { get; set; }

        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await base.ExecuteAsync(cancellationToken);
            response.Headers.Add("Access-Control-Expose-Headers", "X-InlineCount");
            response.Headers.Add("X-InlineCount", XInlineCount);
            return response;
        }
    }
}