using UserService.Entities;
using UserService.Models;
using Newtonsoft.Json;

namespace UserService.Services
{
    public class UserSessionService: IUserSessionService
    {
        private ICacheService _cacheService;
        
        private readonly string SESSION_PREFIX = "SESSION_";

        public UserSessionService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        
       public void CreateSession(User user, string jwt)
        {
            var session = new UserSession {IssuedJwt = jwt, Username = user.Username, Id = user.Id};

            _cacheService.SetValue($"{SESSION_PREFIX}{session.Id.ToString()}", JsonConvert.SerializeObject(session));
        }

        public void deleteSession(string sessionKey)
        {
            _cacheService.Delete($"{SESSION_PREFIX}{sessionKey}");
        }
    }
}