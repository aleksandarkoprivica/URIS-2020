using UserService.Entities;

namespace UserService.Services
{
    public interface IUserSessionService
    {
        void CreateSession(User user, string jwt);

        void deleteSession(string sessionKey);
    }
}