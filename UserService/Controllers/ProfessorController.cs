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
    public class ProfessorController : ControllerBase
    {
       
        private IProfessorService _professorService;
       

        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public IEnumerable<ProfessorResponse> Get()
        {
            return _professorService.GetAll();


        }
        [HttpGet("{id}")]
        public ProfessorResponse Get(Guid Id)
        {
            return _professorService.GetById(Id);


        }

        [HttpPost("register")]
        public ProfessorResponse Create(ProfessorCreateRequest request)
        {
            return _professorService.Create(request);


        }

        [HttpPut("{id}")]
        public ProfessorResponse Put(Guid id, [FromBody]ProfessorUpdateRequest request)
        {

            return _professorService.Update(id, request);

        }

    }
}