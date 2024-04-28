using CoreService.Application.Features.Command.Role.CreateSingleRole;
using CoreService.Application.Features.Queries.Role.GetAllRoles;
using CoreService.Application.Features.Queries.Role.GetSingleRoleById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CoreService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class RolesController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create-single-role")]
        public async Task<CreateSingleRoleCommandResponse> CreateSingleRole([FromBody] CreateSingleRoleCommandRequest requestBody, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(requestBody, cancellationToken);
            this.SetResponseBeforeSend(response);
            return response;
        }

        [HttpGet("get-all-roles-without-relation")]
        public async Task<GetAllRolesWithoutRelationQueryResponse> GetAllRolesWithoutRelation(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllRolesWithoutRelationQueryRequest(), cancellationToken);
            this.SetResponseBeforeSend(response);
            return response;
        }

        [HttpPost("get-single-role-by-id")]
        public async Task<GetSingleRoleByIdQueryResponse> GetSingleRoleById([FromBody] GetSingleRoleByIdQueryRequest requestBody, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(requestBody, cancellationToken);
            this.SetResponseBeforeSend(response);
            return response;
        }
    }
}