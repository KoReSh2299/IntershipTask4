using IntershipTask4.Domain.Entities;
using IntershipTask4.Domain.Filters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.Requests.Queries.Users
{
    public record GetUserByEmailQuery(string Email, Specification<User> Specification) : IRequest<User>;
}
