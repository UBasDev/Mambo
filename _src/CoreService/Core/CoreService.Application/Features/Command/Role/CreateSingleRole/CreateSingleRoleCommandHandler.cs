using CoreService.Application.Models;
using CoreService.Application.Repositories;
using CoreService.Domain.AggregateRoots.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CoreService.Application.Features.Command.Role.CreateSingleRole
{
    internal class CreateSingleRoleCommandHandler(ILogger<CreateSingleRoleCommandHandler> logger, IUnitOfWork unitOfWork) : BaseCqrsAndDomainEventHandler<CreateSingleRoleCommandHandler>(logger), IRequestHandler<CreateSingleRoleCommandRequest, CreateSingleRoleCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateSingleRoleCommandResponse> Handle(CreateSingleRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateSingleRoleCommandResponse();
            try
            {
                if (await _unitOfWork.RoleReadRepository.FindByConditionAsNoTracking(r => r.Name == request.Name || r.ShortCode == request.ShortCode || r.Level == request.Level).AnyAsync(cancellationToken))
                {
                    LogWarning("Role fields must be unique", request, HttpStatusCode.BadRequest);
                    response.SetForError("Role fields must be unique", HttpStatusCode.BadRequest);
                    return response;
                }
                var (roleToCreate, errorMessage) = RoleEntity.CreateNewRole(request.Name, request.ShortCode, request.Level, request.Description);
                if (roleToCreate == null)
                {
                    LogWarning("Unable to create this role", errorMessage, request, HttpStatusCode.BadRequest);
                    response.SetForError(errorMessage, HttpStatusCode.BadRequest);
                    return response;
                }
                await _unitOfWork.RoleWriteRepository.InsertSingleAsync(roleToCreate, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                LogError("Unable to create this role", ex, request, HttpStatusCode.InternalServerError);
                response.SetForError("Unexpected error happened while creating this role", HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}