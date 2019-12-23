using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.DAL;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;
using UserService.Entities;

namespace UserService.Services.Impl
{
    public class ProfessorService:IProfessorService
    {
        private ProfessorDAO _professorDao;
        private IMapper _mapper;
        private IUserService _userService;

        public ProfessorService(ProfessorDAO professorDao, IMapper mapper, IUserService userService)
        {
            _professorDao = professorDao;
            _mapper = mapper;
            _userService = userService;

        }

        public ProfessorResponse Create(ProfessorCreateRequest request)
        {
            Guid ID = _professorDao.InsertProfessor(request);
            var response = new ProfessorResponse(_userService.Create(request, ID), ID,request.University,request.Department,request.Vocation);
            return response;
        }
        //Id=User
        public ProfessorResponse Update(Guid Id, ProfessorUpdateRequest request)
        {
            ProfessorResponse professor = _professorDao.GetById(Id);
            if (!string.IsNullOrWhiteSpace(request.Department))
            {
                professor.Department = request.Department;
            }
            if (!string.IsNullOrWhiteSpace(request.University))
            {
                professor.University = request.University;
            }
            if (!string.IsNullOrWhiteSpace(request.Vocation))
            {
                professor.Vocation = request.Vocation;
            }

            Guid ID=_professorDao.UpdateProfessor(Id, _mapper.Map<ProfessorUpdateRequest>(professor));
            var response = new ProfessorResponse(_userService.UpdateUser(Id,request), ID, professor.University, professor.Department, professor.Vocation);
            return response;
        }
        public IEnumerable<ProfessorResponse> GetAll()
        {
            return _professorDao.GetAllProfessors();
        }
        public ProfessorResponse GetById(Guid Id)
        {
            return _professorDao.GetById(Id);
        }
    }
}
