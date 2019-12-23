using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;

namespace UserService.Services
{
   public  interface IProfessorService
    {
        ProfessorResponse Create(ProfessorCreateRequest request);
        ProfessorResponse Update(Guid Id, ProfessorUpdateRequest request);
        IEnumerable<ProfessorResponse> GetAll();
        ProfessorResponse GetById(Guid Id);
    }
}
