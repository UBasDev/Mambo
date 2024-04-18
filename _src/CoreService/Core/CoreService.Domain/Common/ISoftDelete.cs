using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.Common
{
    public interface ISoftDelete
    {
        protected DateTimeOffset? UpdatedAt { get; }
        protected DateTimeOffset? DeletedAt { get; }
        protected bool IsActive { get; }
        protected bool IsDeleted { get; }
    }
}