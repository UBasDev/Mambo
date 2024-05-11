using CoreService.Application.Features.Command.Screen.CreateSingleScreen;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ScreensController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost("create-single-screen")]
        public async Task<CreateSingleScreenCommandResponse> CreateSingleScreen([FromBody] CreateSingleScreenCommandRequest requestBody)
        {
            return await SetResponseAsync<CreateSingleScreenCommandRequest, CreateSingleScreenCommandResponse>(requestBody);
        }
    }
}