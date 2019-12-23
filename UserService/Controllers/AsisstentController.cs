using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsisstentController : ControllerBase
    {
        private IAssistentService _assitentService;


        public AsisstentController(IAssistentService assistentService)
        {

            _assitentService = assistentService;
        }

        [HttpGet]
        public IEnumerable<AsisstentResponse> Get()
        {
            return _assitentService.GetAll();


        }
        [HttpGet("{id}")]
        public AsisstentResponse Get(Guid Id)
        {
            return _assitentService.GetById(Id);


        }

        [HttpPost("register")]
        public AsisstentResponse Create(AssistentCreateRequest request)
        {
            return _assitentService.Create(request);


        }

        [HttpPut("{id}")]
        public AsisstentResponse Put(Guid id, [FromBody]AssistentUpdateRequest request)
        {

            return _assitentService.Update(id, request);

        }

    }
}