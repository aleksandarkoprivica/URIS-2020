using System;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;

namespace UserService.Services
{
    public interface IUserService
    {
        UserResponse GetById(Guid id);

        AuthenticatedResponse Authenticate(AuthenticateRequest request);

        UserResponse Create(UserCreateRequest request,Guid ID);
        UserResponse UpdateUser(Guid Id, UserUpdateRequest request);
        void ChangePassword(Guid Id, string password);
        void NewPassword(Guid Id);
    }
}