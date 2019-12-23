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
    public class AssistentService:IAssistentService
    {
        private AssistentDAO _assistentDao;
        private IMapper _mapper;
        private IUserService _userService;

        public AssistentService(AssistentDAO assistentDao, IMapper mapper, IUserService userService)
        {
            _assistentDao = assistentDao;
            _mapper = mapper;
            _userService = userService;

        }
        public AsisstentResponse Create(AssistentCreateRequest request)
        {
            Guid ID = _assistentDao.InsertAssistent(request);
            var response = new AsisstentResponse(_userService.Create(request, ID), ID, request.University, request.Department, request.AreaOfStudy);
            return response;
        }
        public AsisstentResponse Update(Guid Id, AssistentUpdateRequest request)
        {
            AsisstentResponse user = _assistentDao.GetById(Id);
            if (!string.IsNullOrWhiteSpace(request.Department))
            {
                user.Department = request.Department;
            }
            if (!string.IsNullOrWhiteSpace(request.University))
            {
                user.University = request.University;
            }
            if (!string.IsNullOrWhiteSpace(request.AreaOfStudy))
            {
                user.AreaOfStudy = request.AreaOfStudy;
            }

            Guid ID = _assistentDao.UpdateAssistent(Id, _mapper.Map<AssistentUpdateRequest>(user));
            var response = new AsisstentResponse(_userService.UpdateUser(Id, request), ID, request.University, request.Department, request.AreaOfStudy);
            return response;
        }
        public IEnumerable<AsisstentResponse> GetAll()
        {
            return _assistentDao.GetAllAssistents();
        }
        public AsisstentResponse GetById(Guid Id)
        {
            return _assistentDao.GetById(Id);
        }
    }
}
