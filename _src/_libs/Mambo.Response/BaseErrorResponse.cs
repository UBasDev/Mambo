using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Response
{
    public class BaseErrorResponse
    {
        public BaseErrorResponse()
        {
            Payload = null;
            IsSuccessful = false;
            ErrorMessage = null;
            RequestId = string.Empty;
            ServerTime = (UInt32)(DateTime.UtcNow.Subtract(DateTime.UnixEpoch)).TotalSeconds;
            StatusCode = HttpStatusCode.InternalServerError;
        }

        private BaseErrorResponse(string errorMessage, string requestId)
        {
            Payload = null;
            IsSuccessful = false;
            ErrorMessage = errorMessage;
            RequestId = requestId;
            ServerTime = (UInt32)(DateTime.UtcNow.Subtract(DateTime.UnixEpoch)).TotalSeconds;
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public object? Payload { get; private set; }
        public bool IsSuccessful { get; private set; }
        public string ErrorMessage { get; private set; }
        public string RequestId { get; private set; }
        public UInt32 ServerTime { get; }
        public HttpStatusCode StatusCode { get; private set; }

        public static BaseErrorResponse CreateNewBaseErrorResponse(string errorMessage, string requestId)
        {
            return new BaseErrorResponse(errorMessage, requestId);
        }
    }
}