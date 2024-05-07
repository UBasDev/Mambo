using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Response
{
    public abstract class BaseResponseMock<T> //This base response will be used for test responses
    {
        protected BaseResponseMock()
        {
            if (typeof(T).IsClass && typeof(T) != typeof(string)) Payload = Activator.CreateInstance<T>();
            else Payload = default;
            IsSuccessful = true;
            ErrorMessage = null;
            RequestId = string.Empty;
            ServerTime = 0;
            StatusCode = HttpStatusCode.OK;
        }

        public T? Payload { get; set; }
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string RequestId { get; set; }
        public UInt32 ServerTime { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public abstract class BaseResponseMock  //This base response will be used for test responses
    {
        protected BaseResponseMock()
        {
            Payload = null;
            IsSuccessful = true;
            ErrorMessage = null;
            RequestId = string.Empty;
            ServerTime = 0;
            StatusCode = HttpStatusCode.OK;
        }

        public object? Payload { get; set; }
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string RequestId { get; set; }
        public UInt32 ServerTime { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}