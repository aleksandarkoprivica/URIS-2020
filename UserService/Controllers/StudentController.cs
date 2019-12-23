using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.DAL;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
       
        private IStudentService _studentService;
       
   
        public StudentController(IStudentService studentService)
        {
          
            _studentService = studentService;
        }

        [HttpGet]
        public IEnumerable<StudentResponse> Get()
        {
            return _studentService.GetAll();


        }
        [HttpGet("{id}")]
        public StudentResponse Get(Guid Id)
        {
            return _studentService.GetById(Id);


        }

        [HttpPost("register")]
        public StudentResponse Create(StudentCreateRequest request)
        {
            return _studentService.Create(request);


        }

        [HttpPut("{id}")]
        public StudentResponse Put(Guid id, [FromBody]StudentUpdateRequest request)
        {

           return  _studentService.Update(id,request);

        }

    }
}