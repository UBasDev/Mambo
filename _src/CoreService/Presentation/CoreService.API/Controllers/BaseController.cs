using Mambo.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected void SetResponseBeforeSend(IBaseResponse response)
        {
            if (!response.IsSuccessful) Response.StatusCode = (int)response.StatusCode;
            response.SetRequestId(HttpContext.TraceIdentifier);
        }
    }
}