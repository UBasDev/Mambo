using CoreService.Application.Models;
using CoreService.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Queries.Role.GetAllRoles
{
    internal class GetAllRolesWithoutRelationQueryHandler(ILogger<GetAllRolesWithoutRelationQueryHandler> logger, IUnitOfWork unitOfWork) : BaseCqrsHandler<GetAllRolesWithoutRelationQueryHandler>(logger, unitOfWork), IRequestHandler<GetAllRolesWithoutRelationQueryRequest, GetAllRolesWithoutRelationQueryResponse>
    {
        public async Task<GetAllRolesWithoutRelationQueryResponse> Handle(GetAllRolesWithoutRelationQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetAllRolesWithoutRelationQueryResponse();
            try
            {
                response.SetPayload(await _unitOfWork.RoleReadRepository.GetAllRolesWithoutRelationAsNoTrackingAsync());
            }
            catch (Exception ex)
            {
                LogError("Unable to get roles", ex, request, HttpStatusCode.InternalServerError);
                response.SetForError("Unexpected error happened while retrieving roles", HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}