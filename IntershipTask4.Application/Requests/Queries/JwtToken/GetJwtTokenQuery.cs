using IntershipTask4.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.Requests.Queries.JwtToken
{
    public record GetJwtTokenQuery(UserForLoginDto Dto) : IRequest<JwtSecurityToken>;
}
