using CoreService.Application.Features.Command.Role.CreateSingleRole;
using CoreService.Application.Features.Queries.Role.GetAllRoles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create-single-role")]
        public async Task<CreateSingleRoleCommandResponse> CreateSingleRole([FromBody] CreateSingleRoleCommandRequest requestBody, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(requestBody, cancellationToken);
            response.SetRequestId(HttpContext.TraceIdentifier);
            if (!response.IsSuccessful) Response.StatusCode = (int)response.StatusCode;
            return response;
        }

        [HttpGet("get-all-roles-without-relation")]
        public async Task<GetAllRolesWithoutRelationQueryResponse> GetAllRolesWithoutRelation()
        {
            var response = await _mediator.Send(new GetAllRolesWithoutRelationQueryRequest());
            response.SetRequestId(HttpContext.TraceIdentifier);
            if (!response.IsSuccessful) Response.StatusCode = (int)response.StatusCode;
            return response;
        }
    }
}