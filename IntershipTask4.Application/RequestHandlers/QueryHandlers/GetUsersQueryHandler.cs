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
    public class GetUsersQueryHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken) =>
            _mapper.Map<IEnumerable<UserDto>>(await _userRepository.Get(true));
    }
}
