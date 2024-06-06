using CoreService.Application.Features.Command.Role.AddSingleScreenToCurrentRoleById;
using CoreService.Application.Features.Command.Role.CreateSingleRole;
using CoreService.Application.Features.Queries.Role.GetAllRoles;
using CoreService.Application.Features.Queries.Role.GetSingleRoleById;
using Mambo.Attribute;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CoreService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [CustomAuthorize("admin", "common")]
    public class RolesController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost("create-single-role")]
        public async Task<CreateSingleRoleCommandResponse> CreateSingleRole([FromBody] CreateSingleRoleCommandRequest requestBody)
        {
            return await SetResponseAsync<CreateSingleRoleCommandRequest, CreateSingleRoleCommandResponse>(requestBody);
        }

        [HttpGet("get-all-roles-without-relation")]
        public async Task<GetAllRolesWithoutRelationQueryResponse> GetAllRolesWithoutRelation()
        {
            return await SetResponseAsync<GetAllRolesWithoutRelationQueryRequest, GetAllRolesWithoutRelationQueryResponse>(new GetAllRolesWithoutRelationQueryRequest());
        }

        [HttpPost("get-single-role-by-id")]
        public async Task<GetSingleRoleByIdQueryResponse> GetSingleRoleById([FromBody] GetSingleRoleByIdQueryRequest requestBody)
        {
            return await SetResponseAsync<GetSingleRoleByIdQueryRequest, GetSingleRoleByIdQueryResponse>(requestBody);
        }

        [HttpPost("add-single-screen-to-current-role")]
        public async Task<AddSingleScreenToCurrentRoleByIdCommandResponse> AddSingleScreenToCurrentRole([FromBody] AddSingleScreenToCurrentRoleByIdCommandRequest requestBody)
        {
            return await SetResponseAsync<AddSingleScreenToCurrentRoleByIdCommandRequest, AddSingleScreenToCurrentRoleByIdCommandResponse>(requestBody);
        }
    }
}