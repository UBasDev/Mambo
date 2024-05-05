using CoreService.Application.Models;
using CoreService.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CoreService.Application.Features.Queries.Role.GetSingleRoleById
{
    internal class GetSingleRoleByIdQueryHandler(ILogger<GetSingleRoleByIdQueryHandler> logger, IUnitOfWork unitOfWork) : BaseCqrsAndDomainEventHandler<GetSingleRoleByIdQueryHandler>(logger), IRequestHandler<GetSingleRoleByIdQueryRequest, GetSingleRoleByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<GetSingleRoleByIdQueryResponse> Handle(GetSingleRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetSingleRoleByIdQueryResponse();
            try
            {
                var foundRole = await _unitOfWork.RoleReadRepository.GetSingleRoleByIdAsNoTrackingAsync(request.Id, cancellationToken);
                if (foundRole == null)
                {
                    LogWarning("We couldn't find this role", request, HttpStatusCode.BadRequest);
                    response.SetForError("We couldn't find this role", HttpStatusCode.BadRequest);
                    return response;
                }
                response.SetPayload(foundRole);
            }
            catch (Exception ex)
            {
                LogError("Unable to retrieve this role", ex, request, HttpStatusCode.InternalServerError);
                response.SetForError("Unexpected error happened while retrieving this role", HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}