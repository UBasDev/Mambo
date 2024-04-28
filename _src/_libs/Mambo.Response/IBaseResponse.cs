using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Response
{
    public interface IBaseResponse
    {
        public bool IsSuccessful { get; }
        protected string? ErrorMessage { get; }
        protected string RequestId { get; }
        public HttpStatusCode StatusCode { get; }
        protected UInt32 ServerTime { get; }

        protected void SetForError(string errorMessage, HttpStatusCode statusCode);

        public void SetRequestId(string requestId);
    }
}