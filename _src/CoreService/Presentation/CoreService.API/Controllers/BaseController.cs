using Mambo.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        protected async Task<TResponse> SetResponse<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse> where TResponse : IBaseResponse
        {
            var response = await _mediator.Send(request);
            if (!response.IsSuccessful) Response.StatusCode = (int)response.StatusCode;
            response.SetRequestId(HttpContext.TraceIdentifier);
            return response;
        }
    }
}