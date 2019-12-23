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
    public class StudentDAO : ConnectionManager
    {

        public StudentDAO(IOptions<AppSettings> settings) : base(settings)
        {

        }

        public Guid InsertStudent(StudentCreateRequest student)
        {
            Create();
            Guid ID = Guid.NewGuid();
            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO  dbo.Student VALUES(@ID,@CreatedAt,@CreatedBy,@LastUpdatedAt,@LastUpdatedBy,@Index)", connection);
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                command.Parameters["@ID"].Value = ID;
                command.Parameters.Add("@Index", SqlDbType.VarChar);
                command.Parameters["@Index"].Value = student.Index;
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
        public Guid UpdateStudent(Guid Id,StudentUpdateRequest student)
        {
               Guid StudentId = Guid.NewGuid();
                Create();
                using (connection)
                {
                    SqlCommand Usercommand = new SqlCommand("Select StudentId From [dbo].[User] WHERE ID = @Id ", connection);
                    Usercommand.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                    Usercommand.Parameters["@Id"].Value = Id;
                    using (var reader = Usercommand.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            StudentId = Guid.Parse(reader["StudentId"].ToString());
                        }

                    }
                if (!string.IsNullOrWhiteSpace(student.Index))
                {
                    SqlCommand command = new SqlCommand("UPDATE dbo.Student SET Index =@Index,LastUpdatedAt=@LastUpdatedAt  WHERE ID = @Id ", connection);
                    command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                    command.Parameters["@Id"].Value = StudentId;

                    command.Parameters.Add("@Index", SqlDbType.VarChar);
                    command.Parameters["@Index"].Value = student.Index;

                    command.Parameters.Add("@LastUpdatedAt", SqlDbType.DateTime);
                    command.Parameters["@LastUpdatedAt"].Value = DateTime.Now;


                    command.ExecuteNonQuery();
                }
                    connection.Close();


                }

            return StudentId;
        }
        public IEnumerable<StudentResponse> GetAllStudents()
        {
            List<StudentResponse> students = new List<StudentResponse>();
            Create();
            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM [dbo].[User] INNER JOIN [dbo].[Student] ON StudentId = [dbo].[Student].Id; ", connection);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        StudentResponse student = new StudentResponse();
                        student.Id = Guid.Parse(reader["Id"].ToString());
                        student.StudentId = Guid.Parse(reader["StudentId"].ToString());
                        student.FirstName = reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.Username = reader["Username"].ToString();
                        student.Email = reader["Email"].ToString();
                        student.Index= reader["Index"].ToString();
                        students.Add(student);


                    }

                }
                Close();
            }

            return students;
        }
        public StudentResponse GetById(Guid Id)
        {
            StudentResponse student = new StudentResponse();
            Create();
            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM [dbo].[User] INNER JOIN [dbo].[Student] ON StudentId = [dbo].[Student].Id where [dbo].[User].Id=@Id", connection);
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters["@Id"].Value = Id;
                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        student.Id = Guid.Parse(reader["Id"].ToString());
                        student.StudentId = Guid.Parse(reader["StudentId"].ToString());
                        student.FirstName = reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.Username = reader["Username"].ToString();
                        student.Email = reader["Email"].ToString();
                        student.Index = reader["Index"].ToString(); ;
                      


                    }

                }
                Close();
            }

            return student;
        }
    }
}
