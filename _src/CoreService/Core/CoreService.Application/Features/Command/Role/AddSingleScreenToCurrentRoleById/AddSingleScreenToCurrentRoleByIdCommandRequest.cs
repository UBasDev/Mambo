using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Command.Role.AddSingleScreenToCurrentRoleById
{
    public sealed class AddSingleScreenToCurrentRoleByIdCommandRequest : IRequest<AddSingleScreenToCurrentRoleByIdCommandResponse>
    {
        public AddSingleScreenToCurrentRoleByIdCommandRequest()
        {
            RoleId = Guid.Empty;
            ScreenId = Guid.Empty;
        }

        public Guid RoleId { get; set; }
        public Guid ScreenId { get; set; }
    }
}