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
    public class UpdateUserCommandHandler(IUserRepository repository) : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _repository = repository;

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(request.Dto.Id, new NotDeletedUserSpecification(), false);
            user.LastLoginTime = request.Dto.LastLoginTime;
            user.IsActive = request.Dto.IsActive;
            _repository.Update(user);
            await _repository.SaveChangesAsync();
        }
    }
}
