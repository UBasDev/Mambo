using CoreService.Application.Models;
using CoreService.Application.Repositories;
using CoreService.Domain.AggregateRoots.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Command.Screen.CreateSingleScreen
{
    internal sealed class CreateSingleScreenCommandHandler(ILogger<CreateSingleScreenCommandHandler> logger, IUnitOfWork unitOfWork) : BaseCqrsAndDomainEventHandler<CreateSingleScreenCommandHandler>(logger), IRequestHandler<CreateSingleScreenCommandRequest, CreateSingleScreenCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateSingleScreenCommandResponse> Handle(CreateSingleScreenCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateSingleScreenCommandResponse();
            try
            {
                if (await _unitOfWork.ScreenReadRepository.FindByConditionAsNoTracking(r => r.Name == request.Name || r.Value == request.Value || r.OrderNumber == request.OrderNumber).AnyAsync(cancellationToken))
                {
                    LogWarning("Screen fields must be unique", request, HttpStatusCode.BadRequest);
                    response.SetForError("Screen fields must be unique", HttpStatusCode.BadRequest);
                    return response;
                }
                var (screenToCreate, errorMessage) = ScreenEntity.CreateNewScreenEntity(request.Name, request.Value, request.OrderNumber);
                if (screenToCreate == null)
                {
                    LogWarning("Unable to create this screen", errorMessage, request, HttpStatusCode.BadRequest);
                    response.SetForError(errorMessage, HttpStatusCode.BadRequest);
                    return response;
                }
                await _unitOfWork.ScreenWriteRepository.InsertSingleAsync(screenToCreate, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                LogError("Unable to create this screen", ex, request, HttpStatusCode.InternalServerError);
                response.SetForError("Unexpected error happened while creating this screen", HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}