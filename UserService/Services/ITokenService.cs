using System;
using UserService.Entities;

namespace UserService.Services
{
    public interface ITokenService
    {
       String GenerateJWT(User user);
    }
}