using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Queries.User.RefreshMyToken
{
    public sealed class RefreshMyTokenQueryRequest : IRequest<RefreshMyTokenQueryResponse>
    {
    }
}