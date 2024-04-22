using CoreService.Application.Features.Command.Role.CreateSingleRole;
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
            return response;
        }
    }
}