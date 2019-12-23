using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.DAL;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;

namespace UserService.Services.Impl
{
    public class StudentService:IStudentService
    {
        private StudentDAO _studentDao;
        private IMapper _mapper;
        private IUserService _userService;

        public StudentService(StudentDAO studentDao, IMapper mapper, IUserService userService)
        {
            _studentDao = studentDao;
            _mapper = mapper;
            _userService = userService;

        }
        public StudentResponse Create(StudentCreateRequest request)
        {
            Guid ID= _studentDao.InsertStudent(request);
            var response = new StudentResponse(_userService.Create(request, ID), request.Index, ID);
            return response;
            

        }
        //Id=UserId
        public StudentResponse Update(Guid Id, StudentUpdateRequest request)
        {

            Guid ID = _studentDao.UpdateStudent(Id, request);
            
            var response = new StudentResponse(_userService.UpdateUser(Id, request), request.Index,ID );
          
            return response;
        }
        public IEnumerable<StudentResponse> GetAll()
        {
           return _studentDao.GetAllStudents();


        }
        //id=userId
        public StudentResponse GetById(Guid Id)
        {
            return _studentDao.GetById(Id);
        }




    }
    }
