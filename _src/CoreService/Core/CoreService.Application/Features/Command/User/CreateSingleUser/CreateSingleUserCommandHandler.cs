using CoreService.Application.Models;
using CoreService.Application.Repositories;
using CoreService.Domain.AggregateRoots.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Command.User.CreateSingleUser
{
    internal class CreateSingleUserCommandHandler(ILogger<CreateSingleUserCommandHandler> logger, IUnitOfWork unitOfWork, IPublisher publisher) : BaseCqrsAndDomainEventHandler<CreateSingleUserCommandHandler>(logger), IRequestHandler<CreateSingleUserCommandRequest, CreateSingleUserCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublisher _publisher = publisher;

        public async Task<CreateSingleUserCommandResponse> Handle(CreateSingleUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateSingleUserCommandResponse();
            try
            {
                var isUsernameOrEmailAlreadyExist = await _unitOfWork.UserReadRepository.FindByConditionAsNoTracking(u => u.Username == request.Username || u.Email == request.Email).AnyAsync(cancellationToken);
                if (isUsernameOrEmailAlreadyExist)
                {
                    LogWarning("This email or username already exists", request, HttpStatusCode.BadRequest);
                    response.SetForError("This email or username already exists", HttpStatusCode.BadRequest);
                    return response;
                }
                var (userToCreate, errorMessage) = UserEntity.CreateNewUserEntity(request.Username, request.Email, request.Password);
                if (userToCreate == null)
                {
                    LogWarning("Unable to create this user", errorMessage, request, HttpStatusCode.BadRequest);
                    response.SetForError(errorMessage, HttpStatusCode.BadRequest);
                    return response;
                }
                var foundDefaultRoleId = await _unitOfWork.RoleReadRepository.FindByConditionAsNoTracking(r => r.Level == 10).Select(r => r.Id).FirstOrDefaultAsync(cancellationToken);
                if (string.IsNullOrEmpty(foundDefaultRoleId.ToString()))
                {
                    LogWarning("Unable to assign default role to this user", request, HttpStatusCode.BadRequest);
                    response.SetForError("Unable to assign default role to this user", HttpStatusCode.BadRequest);
                    return response;
                }
                userToCreate.SetDefaultRoleId(foundDefaultRoleId);
                await _unitOfWork.UserWriteRepository.InsertSingleAsync(userToCreate, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var errorMessageWhileSettingProfile = userToCreate.SetProfileAfterUserCreated(userToCreate.Id, request.Firstname, request.Lastname, request.CompanyName);
                if (errorMessageWhileSettingProfile != null)
                {
                    LogWarning(errorMessageWhileSettingProfile, request, HttpStatusCode.BadRequest);
                    response.SetForError(errorMessageWhileSettingProfile, HttpStatusCode.BadRequest);
                    return response;
                }
                foreach (var currentEvent in userToCreate.DomainEvents)
                {
                    await _publisher.Publish(currentEvent, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                LogError("Unable to create this user", ex, request, HttpStatusCode.InternalServerError);
                response.SetForError("Unexpected error happened while creating this user", HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}