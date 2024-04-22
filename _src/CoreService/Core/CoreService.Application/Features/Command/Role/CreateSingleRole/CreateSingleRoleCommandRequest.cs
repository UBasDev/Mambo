using CoreService.Domain.DomainErrors;
using MediatR;

namespace CoreService.Application.Features.Command.Role.CreateSingleRole
{
    public class CreateSingleRoleCommandRequest : IRequest<CreateSingleRoleCommandResponse>
    {
        public CreateSingleRoleCommandRequest()
        {
            Name = string.Empty;
            ShortCode = string.Empty;
            Level = 0;
            Description = null;
        }

        public string Name { get; set; }
        public string ShortCode { get; set; }
        public UInt16 Level { get; set; }

        public string? Description { get; set; }
    }
}