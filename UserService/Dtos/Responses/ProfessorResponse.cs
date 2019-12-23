using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos.Responses
{
    public class ProfessorResponse:UserResponse
    {
        public Guid ProfessorId { get; set; }
        public string University { get; set; }
        public string Department { get; set; }
        public string Vocation { get; set; }

        public ProfessorResponse(UserResponse ch, Guid professorId,string university,string department, string vocation)
        {
            foreach (var prop in ch.GetType().GetProperties())
            {
                this.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(ch, null), null);
            }
            
            ProfessorId = professorId;
            University = university;
            Department = department;
            Vocation = vocation;
        }

        public ProfessorResponse()
        {

        }
    }
}
