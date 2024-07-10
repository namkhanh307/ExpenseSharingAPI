using AutoMapper;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.PersonGroupModel;
using Services.IServices;

namespace Services.Services
{
    public class PersonGroupService : IPersonGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PersonGroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void DeletePersonGroup(string id)
        {
            throw new NotImplementedException();
        }

        public List<PersonGroup> GetPersonGroups()
        {
            throw new NotImplementedException();
        }

        public void PostPersonGroup(PostPersonGroupModel model)
        {
            throw new NotImplementedException();
        }

        public void PutPersonGroup(string id, PutPersonGroupModel model)
        {
            throw new NotImplementedException();
        }
    }
}
