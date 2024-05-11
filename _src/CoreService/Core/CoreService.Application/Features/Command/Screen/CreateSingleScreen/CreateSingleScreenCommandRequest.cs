using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Command.Screen.CreateSingleScreen
{
    public sealed class CreateSingleScreenCommandRequest : IRequest<CreateSingleScreenCommandResponse>
    {
        public CreateSingleScreenCommandRequest()
        {
            Name = string.Empty;
            Value = string.Empty;
            OrderNumber = 0;
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public UInt16 OrderNumber { get; set; }
    }
}