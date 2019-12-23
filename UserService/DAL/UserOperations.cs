using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos.Responses;
using UserService.Entities;
using UserService.Helpers;

namespace UserService.DAL
{
    public class UserOperations:ConnectionManager
    {
     
        public UserOperations(IOptions<AppSettings> settings):base(settings)
        {
           
        }

        public User GetById(Guid Id)
        {
            User user = new User();
            Create();
            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM [dbo].[User] where ID='"+Id+"'", connection);
               
                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                       
                        user.Id = Id;
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Username = reader["Username"].ToString();
                        user.Email = reader["Email"].ToString();



                    }

                }
                Close();
            }

            return user;
        }

        public void UpdateUser(Guid id,User user)
        {
            Create();
            using (connection)
            {
                SqlCommand command = new SqlCommand("UPDATE [dbo].[User] SET FirstName = @FirstName,LastName=@LastName,Email=@Email WHERE ID = @Id ", connection);

                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters["@Id"].Value = id;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                command.Parameters["@FirstName"].Value = user.FirstName;

                command.Parameters.Add("@LastName", SqlDbType.VarChar);
                command.Parameters["@LastName"].Value = user.LastName;

                command.Parameters.Add("@Email", SqlDbType.VarChar);
                command.Parameters["@Email"].Value = user.Email;

                command.Parameters.Add("@Username", SqlDbType.VarChar);
                command.Parameters["@Username"].Value = user.Username;

                command.ExecuteNonQuery();
                connection.Close();


            }
        }

        public void ChangePassword(Guid Id,string password, byte[] passwordHash,byte[] passwordSalt)
        {
            Create();
            using (connection)
            {
                SqlCommand command = new SqlCommand("UPDATE [dbo].[User] SET Password = @Password,PasswordHash=@PasswordHash,PasswordSalt=@PasswordSalt WHERE ID = @Id ", connection);
                command.Parameters.Add("@Password", SqlDbType.VarChar);
                command.Parameters["@Password"].Value = password;

                command.Parameters.Add("@PasswordHash", SqlDbType.VarBinary);
                command.Parameters["@PasswordHash"].Value = passwordHash;

                command.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary);
                command.Parameters["@PasswordSalt"].Value = passwordSalt;

           
                command.ExecuteNonQuery();
                connection.Close();


            }
        }
        public void InsertUser(User user)
        {
           //add role
            Create();
          
            string ForeignKey="";
            using (connection)
            {
                SqlCommand command = new SqlCommand();
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                command.Parameters["@ID"].Value = user.Id;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                command.Parameters["@FirstName"].Value = user.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar);
                command.Parameters["@LastName"].Value = user.LastName;
                command.Parameters.Add("@Username", SqlDbType.VarChar);
                command.Parameters["@Username"].Value = user.Username;
                command.Parameters.Add("@Email", SqlDbType.VarChar);
                command.Parameters["@Email"].Value = user.Email;
                command.Parameters.Add("@Password", SqlDbType.VarChar);
                command.Parameters["@Password"].Value = user.Password;
                command.Parameters.Add("@PasswordHash", SqlDbType.Binary);
                command.Parameters["@PasswordHash"].Value = user.PasswordHash;
                command.Parameters.Add("@PasswordSalt", SqlDbType.Binary);
                command.Parameters["@PasswordSalt"].Value = user.PasswordSalt;
                command.Parameters.Add("@LastUpdatedAt", SqlDbType.DateTime);
                command.Parameters["@LastUpdatedAt"].Value = DateTime.Now ;
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
                command.Parameters["@CreatedAt"].Value = DateTime.Now;
                command.Parameters.Add("@CreatedBy", SqlDbType.VarChar);
                command.Parameters["@CreatedBy"].Value = "ivona";
                command.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar);
                command.Parameters["@LastUpdatedBy"].Value = "ivona";
                command.Parameters.Add("@Role", SqlDbType.Int);
                command.Parameters["@Role"].Value =user.Role;
                if (user.Role == Roles.Student)
                {
                    command.Parameters.Add("@StudentId", SqlDbType.UniqueIdentifier);
                    command.Parameters["@StudentId"].Value = user.StudentId;
                    ForeignKey = "@StudentId";
                }
                else if (user.Role == Roles.Assistent)
                {
                    command.Parameters.Add("@AssistentId", SqlDbType.UniqueIdentifier);
                    command.Parameters["@AssistentId"].Value = user.AssistentId;
                    ForeignKey = "@AssistentId";
                }
                else if (user.Role == Roles.Professor)
                {
                    command.Parameters.Add("@ProfessorId", SqlDbType.UniqueIdentifier);
                    command.Parameters["@ProfessorId"].Value = user.ProfessorId;
                    ForeignKey = "@ProfessorId";
                }


                command.Connection = connection;
                command.CommandText = "INSERT INTO  [dbo].[User] (Id,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy,FirstName,LastName,Email,Username,Password,PasswordHash,PasswordSalt,Role,"+ string.Concat(ForeignKey.Skip(1)) + ") VALUES(@ID,@CreatedAt,@CreatedBy,@LastUpdatedAt,@LastUpdatedBy,@FirstName,@LastName,@Email,@Username,@Password,@PasswordHash,@PasswordSalt,@Role," + ForeignKey + ")";
                command.ExecuteNonQuery();
                connection.Close();


            }

         
        }
        public User GetByUsername(string Username)
        {
            User user = null;
            Create();
            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM [dbo].[User] where Username='" + Username +"'", connection);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        user = new User();
                        user.Id = Guid.Parse(reader["ID"].ToString());
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Username = reader["Username"].ToString();



                    }

                }
                Close();
            }

            return user;
        }
        public IEnumerable<UserResponse> GetAllUsers()
        {
            List<UserResponse> users = new List<UserResponse>();
            Create();
            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM [dbo].[User]", connection);
              
                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        UserResponse user = new UserResponse();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Username = reader["Username"].ToString();
                        user.Email = reader["Email"].ToString();
                        users.Add(user);


                    }

                }
               Close();
            }
            
            return users;
        }
    }
}
