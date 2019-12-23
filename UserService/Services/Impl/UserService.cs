using System;
using System.Linq;
using System.Text;
using AutoMapper;
using UserService.Database;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;
using UserService.Entities;
using UserService.Models;
using Microsoft.EntityFrameworkCore;
using UserService.DAL;
using System.Collections.Generic;

namespace UserService.Services
{
    public class UserService: IUserService
    {
        

        private ITokenService _tokenService;

        private IUserSessionService _userSessionService;
        private UserOperations _userops;
        private StudentDAO _studentDao;
        private AssistentDAO _assistentDao;
        private ProfessorDAO _professorDao;

        private IMapper _mapper;

        public UserService(ITokenService tokenService, IUserSessionService userSessionService, IMapper mapper, UserOperations ops, StudentDAO studDao, ProfessorDAO profDao, AssistentDAO assisDao)
        {
           
            _tokenService = tokenService;
            _userSessionService = userSessionService;
            _mapper = mapper;
            _userops = ops;
            _studentDao = studDao;
            _assistentDao = assisDao;
            _professorDao = profDao;
        }
        
        public UserResponse GetById(Guid id)
        {
            return _mapper.Map<UserResponse>(_userops.GetById(id));
           
        }
        public AuthenticatedResponse Authenticate(AuthenticateRequest request)
        {
            var username = request.Username;
            var password = request.Password;
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

             var user = _userops.GetByUsername(username);
         

            // check if username exists TODO: add exception
             if (user == null)
                 return null;

             // check if password is correct
             if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                 return null;

             // generate jwt
             var jwt = _tokenService.GenerateJWT(user);

             // map to suitable user response object
             var userResponse = _mapper.Map<UserResponse>(user);

             // create user session and insert in redis
             _userSessionService.CreateSession(user, jwt);

             return new AuthenticatedResponse {Jwt = jwt, User = userResponse};
        }

        public void ChangePassword(Guid Id,string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
           
            
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            _userops.ChangePassword(Id,password, passwordHash, passwordSalt);
        }

        public UserResponse Create(UserCreateRequest request,Guid ID)
        {
            
            // validation
             if (string.IsNullOrWhiteSpace(request.Password))
                  throw new Exception("Password is required");

              if (_userops.GetByUsername(request.Username)!=null)
                  throw new Exception("Username \"" + request.Username + "\" is already taken");

              byte[] passwordHash, passwordSalt;
              CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

              var user = _mapper.Map<User>(request);

              user.PasswordHash = passwordHash;
              user.PasswordSalt = passwordSalt;
              user.Id = Guid.NewGuid();
         
            if(request.Role==Roles.Student)
            {
                user.StudentId = ID;
            }
            else if(request.Role == Roles.Assistent)
            {
                user.AssistentId = ID;
            }
            else if (request.Role == Roles.Professor)
            {
                user.ProfessorId = ID;
            }

            // TODO: remove user password from db model
            _userops.InsertUser(user);
              // TODO: add rabbitmq -> leave for now

              return _mapper.Map<UserResponse>(user);
           
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
           
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
              if (password == null) throw new ArgumentNullException(nameof(password));
              if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
              if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
              if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(storedSalt));

              using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
              {
                  var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                  for (int i = 0; i < computedHash.Length; i++)
                  {
                      if (computedHash[i] != storedHash[i]) return false;
                  }
              }

              return true;
           
        }

        public void NewPassword(Guid Id)
        {
            string newPassword=GenerateRandomPassword();
            ChangePassword(Id, newPassword);
            //send e-mail with new password

        }

        public IEnumerable<UserResponse> GetAll()
        {
            return _userops.GetAllUsers();
        }

        private string GenerateRandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            Random random = new Random(); 
            builder.Append(random.Next(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public UserResponse UpdateUser(Guid Id, UserUpdateRequest request)
        {
            User user = _userops.GetById(Id);
            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                user.FirstName = request.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                user.LastName = request.LastName;
            }
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                user.Email = request.Email;
            }
            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                if (!user.Username.Equals(request.Username))
                {
                    if (_userops.GetByUsername(request.Username) != null)
                        throw new Exception("Username \"" + request.Username + "\" is already taken");
                    user.Username = request.Username;
                }
            }
            

            _userops.UpdateUser(Id, user);
            return _mapper.Map<UserResponse>(user);

        }

       

    }
}