using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Command.User.CreateSingleUser
{
    internal class CreateSingleUserCommandHandler : IRequestHandler<CreateSingleUserCommandRequest, CreateSingleUserCommandResponse>
    {
        public async Task<CreateSingleUserCommandResponse> Handle(CreateSingleUserCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}