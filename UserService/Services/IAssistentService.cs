using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;

namespace UserService.Services
{
    public interface IAssistentService
    {
        AsisstentResponse Create(AssistentCreateRequest request);
        AsisstentResponse Update(Guid Id, AssistentUpdateRequest request);
        IEnumerable<AsisstentResponse> GetAll();
        AsisstentResponse GetById(Guid Id);
    }
}
