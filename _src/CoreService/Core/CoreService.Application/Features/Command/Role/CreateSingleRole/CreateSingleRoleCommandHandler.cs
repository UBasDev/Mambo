using CoreService.Application.Repositories;
using CoreService.Domain.AggregateRoots.User;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CoreService.Application.Features.Command.Role.CreateSingleRole
{
    public class CreateSingleRoleCommandHandler(ILogger<CreateSingleRoleCommandHandler> logger, IUnitOfWork unitOfWork) : IRequestHandler<CreateSingleRoleCommandRequest, CreateSingleRoleCommandResponse>
    {
        private readonly ILogger<CreateSingleRoleCommandHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateSingleRoleCommandResponse> Handle(CreateSingleRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateSingleRoleCommandResponse();
            try
            {
                var (roleToCreate, errorMessage) = RoleEntity.CreateNewRole(request.Name, request.ShortCode, request.Level, request.Description);
                if (roleToCreate == null)
                {
                    _logger.LogError("Unable to create this role. Request: {@Request} Error: {@Error}", request, errorMessage);
                    response.SetForError(errorMessage, HttpStatusCode.BadRequest);
                    return response;
                }
                await _unitOfWork.RoleWriteRepository.InsertSingleAsync(roleToCreate, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to create this role. Request: {@Request} Error: {@Error}", request, ex);
                response.SetForError("Unexpected error happened while creating this role", HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}