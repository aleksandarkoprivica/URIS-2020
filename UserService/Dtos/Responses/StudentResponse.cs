using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos.Responses
{
    public class StudentResponse:UserResponse
    {
        public Guid StudentId { get; set; }
        public string Index { get; set; }

        public StudentResponse()
        {

        }
        public StudentResponse(UserResponse ch, string index,Guid studentId)
        {
            foreach (var prop in ch.GetType().GetProperties())
            {
                this.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(ch, null), null);
            }
            Index = index;
            StudentId = studentId;
        }
    }
}
