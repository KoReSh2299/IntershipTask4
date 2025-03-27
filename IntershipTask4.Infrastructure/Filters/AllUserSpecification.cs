using IntershipTask4.Domain.Entities;
using IntershipTask4.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Infrastructure.Filters
{
    public class AllUserSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => true;
        }
    }
}
