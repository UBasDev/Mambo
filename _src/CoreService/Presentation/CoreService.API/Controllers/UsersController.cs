using CoreService.Application.Features.Command.User.CreateSingleUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class UsersController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost("create-single-user")]
        public async Task<CreateSingleUserCommandResponse> CreateSingleUser([FromBody] CreateSingleUserCommandRequest requestBody)
        {
            return await SetResponse<CreateSingleUserCommandRequest, CreateSingleUserCommandResponse>(requestBody);
        }
    }
}