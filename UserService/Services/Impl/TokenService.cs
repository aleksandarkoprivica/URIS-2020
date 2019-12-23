using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Entities;
using UserService.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace UserService.Services
{
    public class TokenService: ITokenService
    {
        private string _secret;

        private JwtSecurityTokenHandler _tokenHandler;
        
        public TokenService(IOptions<AppSettings> op)
        {
            _secret = op.Value.Secret;
            _tokenHandler = new JwtSecurityTokenHandler();
        }
        
        public string GenerateJWT(User user)
        {
            var key = Encoding.ASCII.GetBytes(_secret);
            // TODO: Add roles
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = _tokenHandler.CreateToken(tokenDescriptor);
            
            return _tokenHandler.WriteToken(token);
        }
    }
}