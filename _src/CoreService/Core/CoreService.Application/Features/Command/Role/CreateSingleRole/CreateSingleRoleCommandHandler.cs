using CoreService.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoreService.Application.Features.Command.Role.CreateSingleRole
{
    public class CreateSingleRoleCommandHandler(ILogger<CreateSingleRoleCommandHandler> logger, IUnitOfWork unitOfWork) : IRequestHandler<CreateSingleRoleCommandRequest, CreateSingleRoleCommandResponse>
    {
        private readonly ILogger<CreateSingleRoleCommandHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateSingleRoleCommandResponse> Handle(CreateSingleRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateSingleRoleCommandResponse();
            var x1 = await _unitOfWork.RoleReadRepository.GetAllAsNoTrackingAsync(cancellationToken);
            return response;
        }
    }
}