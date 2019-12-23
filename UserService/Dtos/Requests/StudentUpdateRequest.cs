using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos.Requests
{
   public class StudentUpdateRequest:UserUpdateRequest
    {
        public string Index { get; set; }
    }
}
