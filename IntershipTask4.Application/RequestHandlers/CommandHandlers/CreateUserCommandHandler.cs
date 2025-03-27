using AutoMapper;
using IntershipTask4.Application.Requests.Commands;
using IntershipTask4.Domain.abstractions;
using IntershipTask4.Domain.Entities;
using IntershipTask4.Infrastructure.Filters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.RequestHandlers.CommandHandlers
{
    public class CreateUserCommandHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmail(request.Dto.Email, new NotBlockedUserSpecification(), false);
            if (user == null)
            {
                await _repository.Create(_mapper.Map<User>(request.Dto));
                await _repository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Account with this email is already exist.");
            }
        }
    }
}
