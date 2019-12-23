using UserService.Entities;
using System;

namespace UserService.Models
{
    public class UserSession
    {
        public string IssuedJwt { get; set; }

        public Guid Id { get; set; }

        public string Username { get; set; }
        
        public Roles Role { get; set; }
        // TODO: Add Roles
    }
}