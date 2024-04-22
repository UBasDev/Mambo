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
    internal class GetAllRolesWithoutRelationQueryHandler(ILogger<GetAllRolesWithoutRelationQueryHandler> logger, IUnitOfWork unitOfWork) : IRequestHandler<GetAllRolesWithoutRelationQueryRequest, GetAllRolesWithoutRelationQueryResponse>
    {
        private readonly ILogger<GetAllRolesWithoutRelationQueryHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<GetAllRolesWithoutRelationQueryResponse> Handle(GetAllRolesWithoutRelationQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetAllRolesWithoutRelationQueryResponse();
            try
            {
                response.SetPayload(await _unitOfWork.RoleReadRepository.GetAllRolesWithoutRelationAsNoTrackingAsync());
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