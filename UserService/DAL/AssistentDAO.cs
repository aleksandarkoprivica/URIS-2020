using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;
using UserService.Entities;
using UserService.Helpers;

namespace UserService.DAL
{
    public class AssistentDAO:ConnectionManager
    {

        public AssistentDAO(IOptions<AppSettings> settings) : base(settings)
        {

        }

        public Guid InsertAssistent(AssistentCreateRequest assistent)
        {
            Create();
            Guid ID = Guid.NewGuid();
            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO  dbo.Assistent VALUES(@ID,@CreatedAt,@CreatedBy,@LastUpdatedAt,@LastUpdatedBy,@University,@Department,@AreaOfStudy)", connection);
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                command.Parameters["@ID"].Value = ID;
                command.Parameters.Add("@University", SqlDbType.VarChar);
                command.Parameters["@University"].Value = assistent.University;
                command.Parameters.Add("@Department", SqlDbType.VarChar);
                command.Parameters["@Department"].Value = assistent.Department;
                command.Parameters.Add("@AreaOfStudy", SqlDbType.VarChar);
                command.Parameters["@AreaOfStudy"].Value = assistent.AreaOfStudy;
                command.Parameters.Add("@LastUpdatedAt", SqlDbType.DateTime);
                command.Parameters["@LastUpdatedAt"].Value = DateTime.Now;
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
                command.Parameters["@CreatedAt"].Value = DateTime.Now;
                command.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar);
                command.Parameters["@LastUpdatedBy"].Value = "ivona";
                command.Parameters.Add("@CreatedBy", SqlDbType.VarChar);
                command.Parameters["@CreatedBy"].Value = "ivona";

                command.ExecuteNonQuery();
                connection.Close();


            }

            return ID;
        }

       
        public Guid UpdateAssistent(Guid Id, AssistentUpdateRequest assistent)
        {
            Guid AssistentId = Guid.NewGuid();
            Create();
            using (connection)
            {
                SqlCommand Usercommand = new SqlCommand("Select AssistentId From [dbo].[User] WHERE ID = @Id ", connection);
                Usercommand.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                Usercommand.Parameters["@Id"].Value = Id;
                using (var reader = Usercommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        AssistentId = Guid.Parse(reader["StudentId"].ToString());
                    }

                }
                 SqlCommand command = new SqlCommand("UPDATE dbo.Assistent SET University =@University, AreaOfStudy =@AreaOfStudy,Department =@Department,LastUpdatedAt=@LastUpdatedAt  WHERE ID = @Id ", connection);
                    command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                    command.Parameters["@Id"].Value = AssistentId;

                    command.Parameters.Add("@Department", SqlDbType.VarChar);
                    command.Parameters["@Department"].Value = assistent.Department;

                    command.Parameters.Add("@University", SqlDbType.VarChar);
                    command.Parameters["@University"].Value = assistent.University;
                    command.Parameters.Add("@AreaOfStudy", SqlDbType.VarChar);
                    command.Parameters["@AreaOfStudy"].Value = assistent.AreaOfStudy;

                    command.Parameters.Add("@LastUpdatedAt", SqlDbType.DateTime);
                    command.Parameters["@LastUpdatedAt"].Value = DateTime.Now;


                    command.ExecuteNonQuery();
                
                connection.Close();


            }

            return AssistentId;
        }
        public IEnumerable<AsisstentResponse> GetAllAssistents()
        {
            List<AsisstentResponse> assistents = new List<AsisstentResponse>();
            Create();
            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM [dbo].[User] INNER JOIN [dbo].[Assistent] ON AssistentId = [dbo].[Assistent].Id; ", connection);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        AsisstentResponse assistent = new AsisstentResponse();
                        assistent.Id = Guid.Parse(reader["Id"].ToString());
                        assistent.AsisstentId = Guid.Parse(reader["AssistentId"].ToString());
                        assistent.FirstName = reader["FirstName"].ToString();
                        assistent.LastName = reader["LastName"].ToString();
                        assistent.Username = reader["Username"].ToString();
                        assistent.Email = reader["Email"].ToString();
                        assistent.AreaOfStudy = reader["AreaOfStudy"].ToString();
                        assistent.Department = reader["Department"].ToString();
                        assistent.University = reader["University"].ToString();

                        assistents.Add(assistent);


                    }

                }
                Close();
            }

            return assistents;
        }
        public AsisstentResponse GetById(Guid Id)
        {
            AsisstentResponse assistent = new AsisstentResponse();
            Create();
            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM [dbo].[User] INNER JOIN [dbo].[Assistent] ON AssistentId = [dbo].[Assistent].Id where [dbo].[User].Id=@Id", connection);
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters["@Id"].Value = Id;
                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        assistent.Id = Guid.Parse(reader["Id"].ToString());
                        assistent.AsisstentId = Guid.Parse(reader["AssistentId"].ToString());
                        assistent.FirstName = reader["FirstName"].ToString();
                        assistent.LastName = reader["LastName"].ToString();
                        assistent.Username = reader["Username"].ToString();
                        assistent.Email = reader["Email"].ToString();
                        assistent.AreaOfStudy = reader["AreaOfStudy"].ToString();
                        assistent.Department = reader["Department"].ToString();
                        assistent.University = reader["University"].ToString();



                    }

                }
                Close();
            }

            return assistent;
        }
    }
}
