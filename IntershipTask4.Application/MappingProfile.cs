using AutoMapper;
using IntershipTask4.Application.Dtos;
using IntershipTask4.Application.Services;
using IntershipTask4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, User>()
                .ConvertUsing((src, dst) =>
                {
                    var (hashPass, passSalt) = PasswordHelper.HashPassword(src.Password);

                    var user = new User()
                    {
                        Email = src.Email,
                        IsActive = true,
                        LastLoginTime = null,
                        DeletedAt = null,
                        Name = src.Name,
                        PasswordHash = hashPass,
                        PasswordSalt = passSalt
                    };
                    return user;
                });
            CreateMap<UserForUpdateDto, User>(); 
            CreateMap<UserDto, UserForUpdateDto>();
        }
    }
}
