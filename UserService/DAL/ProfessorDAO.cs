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
    public class ProfessorDAO : ConnectionManager
    {

        public ProfessorDAO(IOptions<AppSettings> settings) : base(settings)
        {

        }

        public Guid InsertProfessor(ProfessorCreateRequest professor)
        {
            Create();
            Guid ID = Guid.NewGuid();
            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO  dbo.Professor VALUES(@ID,@CreatedAt,@CreatedBy,@LastUpdatedAt,@LastUpdatedBy,@University,@Department,@Vocation)", connection);
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                command.Parameters["@ID"].Value = ID;
                command.Parameters.Add("@University", SqlDbType.VarChar);
                command.Parameters["@University"].Value = professor.University;
                command.Parameters.Add("@Department", SqlDbType.VarChar);
                command.Parameters["@Department"].Value = professor.Department;
                command.Parameters.Add("@Vocation", SqlDbType.VarChar);
                command.Parameters["@Vocation"].Value = professor.Vocation;
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

       
        public Guid UpdateProfessor(Guid Id, ProfessorUpdateRequest professor)
        {
            Guid ProfessorId = Guid.NewGuid();
            Create();
            using (connection)
            {
                SqlCommand Usercommand = new SqlCommand("Select ProfessorId From [dbo].[User] WHERE ID = @Id ", connection);
                Usercommand.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                Usercommand.Parameters["@Id"].Value = Id;
                using (var reader = Usercommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        ProfessorId = Guid.Parse(reader["professorId"].ToString());
                    }

                }


                    SqlCommand command = new SqlCommand("UPDATE dbo.Professor SET University=@University,Vocation=@Vocation,Department=@Department,LastUpdatedAt=@LastUpdatedAt  WHERE ID = @Id ", connection);
                    command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                    command.Parameters["@Id"].Value = ProfessorId;

                    command.Parameters.Add("@University", SqlDbType.VarChar);
                    command.Parameters["@University"].Value = professor.University;
                    command.Parameters.Add("@Vocation", SqlDbType.VarChar);
                    command.Parameters["@Vocation"].Value = professor.Vocation;
                    command.Parameters.Add("@Department", SqlDbType.VarChar);
                    command.Parameters["@Department"].Value = professor.Department;

                    command.Parameters.Add("@LastUpdatedAt", SqlDbType.DateTime);
                    command.Parameters["@LastUpdatedAt"].Value = DateTime.Now;


                    command.ExecuteNonQuery();
                
                connection.Close();


            }

            return ProfessorId;
        }
        public IEnumerable<ProfessorResponse> GetAllProfessors()
        {
            List<ProfessorResponse> professors = new List<ProfessorResponse>();
            Create();
            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM [dbo].[User] INNER JOIN [dbo].[Professor] ON ProfessorId = [dbo].[Professor].Id; ", connection);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        ProfessorResponse professor = new ProfessorResponse();
                        professor.Id = Guid.Parse(reader["Id"].ToString());
                        professor.ProfessorId = Guid.Parse(reader["ProfessorId"].ToString());
                        professor.FirstName = reader["FirstName"].ToString();
                        professor.LastName = reader["LastName"].ToString();
                        professor.Username = reader["Username"].ToString();
                        professor.Email = reader["Email"].ToString();
                        professor.University= reader["University"].ToString();
                        professor.Vocation = reader["Vocation"].ToString();
                        professor.Department = reader["Department"].ToString();

                        professors.Add(professor);


                    }

                }
                Close();
            }

            return professors;
        }
        public ProfessorResponse GetById(Guid Id)
        {
            ProfessorResponse professor = new ProfessorResponse();
            Create();
            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM [dbo].[User] INNER JOIN [dbo].[Professor] ON ProfessorId = [dbo].[Professor].Id where [dbo].[User].Id=@Id", connection);
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters["@Id"].Value = Id;
                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        professor.Id = Guid.Parse(reader["Id"].ToString());
                        professor.ProfessorId = Guid.Parse(reader["ProfessorId"].ToString());
                        professor.FirstName = reader["FirstName"].ToString();
                        professor.LastName = reader["LastName"].ToString();
                        professor.Username = reader["Username"].ToString();
                        professor.Email = reader["Email"].ToString();
                        professor.University = reader["University"].ToString();
                        professor.Vocation = reader["Vocation"].ToString();
                        professor.Department = reader["Department"].ToString();



                    }

                }
                Close();
            }

            return professor;
        }

    }
}
