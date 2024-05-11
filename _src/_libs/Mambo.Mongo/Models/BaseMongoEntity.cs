using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Mongo.Models
{
    public abstract class BaseMongoEntity<T>
    {
        public T Id { get; protected set; }
    }
}