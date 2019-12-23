using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos.Requests
{
    public class ProfessorUpdateRequest:UserUpdateRequest
    {
        public string University { get; set; }
        public string Department { get; set; }
        public string Vocation { get; set; }
    }
}
