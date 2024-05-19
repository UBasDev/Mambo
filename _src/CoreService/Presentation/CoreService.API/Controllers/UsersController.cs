using CoreService.Application.Features.Command.User.CreateSingleUser;
using CoreService.Application.Features.Command.User.SignIn;
using CoreService.Application.Features.Queries.User.RefreshMyToken;
using Mambo.Attribute;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class UsersController(IMediator mediator) : BaseController(mediator)
    {
        [CustomAuthorize("admin")]
        [HttpPost("create-single-user")]
        public async Task<CreateSingleUserCommandResponse> CreateSingleUser([FromBody] CreateSingleUserCommandRequest requestBody)
        {
            return await SetResponseAsync<CreateSingleUserCommandRequest, CreateSingleUserCommandResponse>(requestBody);
        }

        [HttpPost("sign-in")]
        public async Task<SignInCommandResponse> SignIn([FromBody] SignInCommandRequest requestBody)
        {
            return await SetResponseAsync<SignInCommandRequest, SignInCommandResponse>(requestBody);
        }

        [HttpGet("refresh-my-token")]
        public async Task<RefreshMyTokenQueryResponse> RefreshMyToken()
        {
            return await SetResponseAsync<RefreshMyTokenQueryRequest, RefreshMyTokenQueryResponse>(new RefreshMyTokenQueryRequest());
        }
    }
}