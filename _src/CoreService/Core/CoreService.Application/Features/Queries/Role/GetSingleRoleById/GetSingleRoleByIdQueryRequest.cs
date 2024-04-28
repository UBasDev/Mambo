using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Queries.Role.GetSingleRoleById
{
    public class GetSingleRoleByIdQueryRequest : IRequest<GetSingleRoleByIdQueryResponse>
    {
        public GetSingleRoleByIdQueryRequest()
        {
            Id = Guid.Empty;
        }

        public Guid Id { get; set; }
    }
}