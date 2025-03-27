using AutoMapper;
using IntershipTask4.Application.Dtos;
using IntershipTask4.Application.Requests.Queries.Users;
using IntershipTask4.Domain.abstractions;
using IntershipTask4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.RequestHandlers.QueryHandlers.Users
{
    public class GetUserByEmailQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByEmailQuery, User?>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<User?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken) =>
            await _repository.GetByEmail(request.Email, request.Specification, false);
    }
}
