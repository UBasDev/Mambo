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
    public class RolesController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost("create-single-role")]
        public async Task<CreateSingleRoleCommandResponse> CreateSingleRole([FromBody] CreateSingleRoleCommandRequest requestBody, CancellationToken cancellationToken)
        {
            return await SetResponse<CreateSingleRoleCommandRequest, CreateSingleRoleCommandResponse>(requestBody);
        }

        [HttpGet("get-all-roles-without-relation")]
        public async Task<GetAllRolesWithoutRelationQueryResponse> GetAllRolesWithoutRelation(CancellationToken cancellationToken)
        {
            return await SetResponse<GetAllRolesWithoutRelationQueryRequest, GetAllRolesWithoutRelationQueryResponse>(new GetAllRolesWithoutRelationQueryRequest());
        }

        [HttpPost("get-single-role-by-id")]
        public async Task<GetSingleRoleByIdQueryResponse> GetSingleRoleById([FromBody] GetSingleRoleByIdQueryRequest requestBody, CancellationToken cancellationToken)
        {
            return await SetResponse<GetSingleRoleByIdQueryRequest, GetSingleRoleByIdQueryResponse>(requestBody);
        }
    }
}