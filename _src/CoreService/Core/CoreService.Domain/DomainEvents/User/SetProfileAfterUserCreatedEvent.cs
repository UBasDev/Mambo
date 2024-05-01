using CoreService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.DomainEvents.User
{
    public class SetProfileAfterUserCreatedEvent(Guid userId, string firstname, string lastname, string companyName) : IDomainEvent
    {
        public Guid UserId { get; } = userId;
        public string Firstname { get; } = firstname;
        public string Lastname { get; } = lastname;
        public string CompanyName { get; } = companyName;
    }
}