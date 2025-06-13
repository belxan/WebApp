using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class UserMappingProfile:Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserResponseDto>().ReverseMap();
        CreateMap<CreateUserRequestDto,User>();
    }
}
