using AutoMapper;
using IntershipTask4.Application.Requests.Commands;
using IntershipTask4.Domain.abstractions;
using IntershipTask4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.RequestHandlers.CommandHandlers
{
    public class UpdateUserCommandHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _repository.Update(_mapper.Map<User>(request.Dto));
            await _repository.SaveChangesAsync();
        }
    }
}
