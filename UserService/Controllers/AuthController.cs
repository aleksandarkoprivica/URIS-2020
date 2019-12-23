using UserService.Dtos.Requests;
using UserService.Dtos.Responses;
using UserService.Services;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        // TODO: Move this function in new AuthService
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // TODO: Wrap with http status
        [HttpPost("login")]
        public AuthenticatedResponse Authenticate(AuthenticateRequest request)
        {
            return _userService.Authenticate(request);
        }
        
      
    }
}