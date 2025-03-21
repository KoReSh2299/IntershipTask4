using AutoMapper;
using IntershipTask4.Application.Dtos;
using IntershipTask4.Application.Requests.Queries;
using IntershipTask4.Domain.abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.RequestHandlers.QueryHandlers
{
    public class GetUserByIdQueryHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
            _mapper.Map<UserDto>(await _repository.Get(request.Id, true));
    }
}
