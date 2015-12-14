using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace FanSelector.Api.Setup.Core
{
    internal class ApiTimedEvent : IDisposable
    {
        private readonly string _event;
        private readonly string _sourceFilePath;
        private readonly int _sourceLineNumber;
        private readonly object[] _memberParameters;
        private readonly Stopwatch _watch = new Stopwatch();
        private string _ipAddress;

        protected internal string IpAddress
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ipAddress) && HttpContext.Current != null)
                    _ipAddress = HttpContext.Current.Request.UserHostAddress;

                return _ipAddress;
            }
            set { _ipAddress = value; }
        }

        public ApiTimedEvent([CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            params object[] memberParameters)
        {
            _event = memberName;
            _sourceFilePath = sourceFilePath;
            _sourceLineNumber = sourceLineNumber;
            _memberParameters = memberParameters;
            _watch.Start();
        }

        public ApiTimedEvent(bool withParams, string memberName,
            string sourceFilePath,
            int sourceLineNumber,
            params object[] memberParameters)
        {
            _event = memberName;
            _sourceFilePath = sourceFilePath;
            _sourceLineNumber = sourceLineNumber;
            _memberParameters = memberParameters;
            _watch.Start();
        }

        #region IDisposable Members

        public void Dispose()
        {
            _watch.Stop();
            //var messageType = string.Empty;
            //var sendingFacility = string.Empty;
            //var idIdentifier = string.Empty;
            //var messageId = string.Empty;

            //var requestLogInfo = new Info(string.Format("Time to Complete Request is: {0} from {1}: with event: {2} and source path: {3} with source line number: {4} and member params: {5}", _watch.ElapsedMilliseconds, IpAddress, _event, _sourceFilePath, _sourceLineNumber, _memberParameters.FirstOrDefault()));
            //var info = new Error(requestLogInfo)
            //{
            //    User = (!string.IsNullOrEmpty(sendingFacility)) ? sendingFacility + (!string.IsNullOrWhiteSpace(idIdentifier) ? " : " + idIdentifier : "") + (!string.IsNullOrWhiteSpace(messageId) ? " : " + messageId : "") : HttpContext.Current.User.Identity.Name,
            //    Source = messageType
            //};
            //ErrorLog.GetDefault(HttpContext.Current).Log(info);
        }

        #endregion
    }
}