using IntershipTask4.Application.Requests.Queries.JwtToken;
using IntershipTask4.Application.Services;
using IntershipTask4.Domain.abstractions;
using IntershipTask4.Infrastructure.Filters;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.RequestHandlers.QueryHandlers.JwtToken
{
    public class GetJwtTokenQueryHandler(IUserRepository userRepository, IConfiguration configuration) : IRequestHandler<GetJwtTokenQuery, JwtSecurityToken>
    {
        private IUserRepository _userRepository = userRepository;
        private IConfiguration _configuration = configuration;

        public async Task<JwtSecurityToken> Handle(GetJwtTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Dto.Email, new NotBlockedUserSpecification(), false) ?? throw new UnauthorizedAccessException("User with such email don't exist.");
            if (PasswordHelper.VerifyPassword(request.Dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                var tokenService = new TokenService(_configuration);
                return tokenService.GenerateToken(user);
            }
            else
            {
                throw new UnauthorizedAccessException("Incorrect password.");
            }
        }
    }
}
