using IntershipTask4.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.Requests.Queries
{
    public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;
}
