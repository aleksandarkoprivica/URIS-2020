using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos.Responses
{
    public class AsisstentResponse:UserResponse
    {
        public Guid AsisstentId { get; set; }
        public string University { get; set; }
        public string Department { get; set; }
        public string AreaOfStudy { get; set; }

        public AsisstentResponse(UserResponse ch, Guid asistentId, string university, string department, string areaOfStudy)
        {
            foreach (var prop in ch.GetType().GetProperties())
            {
                this.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(ch, null), null);
            }

            AsisstentId = asistentId;
            University = university;
            Department = department;
            AreaOfStudy = areaOfStudy;
        }
        public AsisstentResponse()
        {

        }
    }
}
