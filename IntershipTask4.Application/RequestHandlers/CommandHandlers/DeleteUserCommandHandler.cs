using AutoMapper;
using IntershipTask4.Application.Requests.Commands;
using IntershipTask4.Domain.abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.RequestHandlers.CommandHandlers
{
    public class DeleteUserCommandHandler(IUserRepository repository) : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = _repository.Delete(request.Dto.Id);
            await _repository.SaveChangesAsync();
            return result;
        }
    }
}
