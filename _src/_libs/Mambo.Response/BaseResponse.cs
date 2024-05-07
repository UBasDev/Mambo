using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Response
{
    public abstract class BaseResponse : IBaseResponse
    {
        protected BaseResponse()
        {
            Payload = null;
            IsSuccessful = true;
            ErrorMessage = null;
            RequestId = string.Empty;
            ServerTime = (UInt32)(DateTime.UtcNow.Subtract(DateTime.UnixEpoch)).TotalSeconds;
            StatusCode = HttpStatusCode.OK;
        }

        public object? Payload { get; private set; }
        public bool IsSuccessful { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string RequestId { get; private set; }
        public UInt32 ServerTime { get; }
        public HttpStatusCode StatusCode { get; private set; }

        public void SetForError(string errorMessage, HttpStatusCode statusCode)
        {
            IsSuccessful = false;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public void SetRequestId(string requestId) => RequestId = requestId;
    }

    public abstract class BaseResponse<T> : IBaseResponse
    {
        protected BaseResponse()
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string)) Payload = Activator.CreateInstance<T>();
            else Payload = default;
            IsSuccessful = true;
            ErrorMessage = null;
            RequestId = string.Empty;
            ServerTime = (UInt32)(DateTime.UtcNow.Subtract(DateTime.UnixEpoch)).TotalSeconds;
            StatusCode = HttpStatusCode.OK;
        }

        public T? Payload { get; private set; }
        public bool IsSuccessful { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string RequestId { get; private set; }
        public UInt32 ServerTime { get; }
        public HttpStatusCode StatusCode { get; private set; }

        public void SetPayload(T payload)
        {
            Payload = payload;
        }

        public void SetForError(string errorMessage, HttpStatusCode statusCode)
        {
            IsSuccessful = false;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public void SetRequestId(string requestId) => RequestId = requestId;
    }
}