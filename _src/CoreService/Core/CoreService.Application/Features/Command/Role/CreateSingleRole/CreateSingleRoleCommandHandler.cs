using MediatR;
using Microsoft.Extensions.Logging;

namespace CoreService.Application.Features.Command.Role.CreateSingleRole
{
    public class CreateSingleRoleCommandHandler(ILogger<CreateSingleRoleCommandHandler> logger) : IRequestHandler<CreateSingleRoleCommandRequest, CreateSingleRoleCommandResponse>
    {
        private readonly ILogger<CreateSingleRoleCommandHandler> _logger = logger;

        public async Task<CreateSingleRoleCommandResponse> Handle(CreateSingleRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateSingleRoleCommandResponse();
            return response;
        }
    }
}