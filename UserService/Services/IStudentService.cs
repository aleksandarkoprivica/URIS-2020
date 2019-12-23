using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;

namespace UserService.Services
{
    public interface IStudentService
    {
        StudentResponse Create(StudentCreateRequest request);
        StudentResponse Update(Guid Id, StudentUpdateRequest request);
        IEnumerable<StudentResponse> GetAll();
        StudentResponse GetById(Guid Id);
    }
}
