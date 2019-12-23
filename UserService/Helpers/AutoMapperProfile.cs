using AutoMapper;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;
using UserService.Entities;

namespace UserService.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserCreateRequest, User>();
            CreateMap<ProfessorResponse, ProfessorUpdateRequest>();
            CreateMap<AsisstentResponse, AssistentUpdateRequest>();
        }
    }
}