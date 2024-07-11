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

        public List<PersonGroup> GetPersonGroups()
        {
            return _unitOfWork.GetRepository<PersonGroup>().Entities.Where(g => !g.DeletedTime.HasValue).ToList();
        }

        public void PostPersonGroup(PostPersonGroupModel model)
        {
            var personGroup = _mapper.Map<PersonGroup>(model);
            personGroup.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonGroup>().Insert(personGroup);
            _unitOfWork.Save();
        }

        public void PutPersonGroup(string id, PutPersonGroupModel model)
        {
            var existedPersonGroup = _unitOfWork.GetRepository<PersonGroup>().GetById(id);
            if (existedPersonGroup == null)
            {
                throw new Exception($"PersonGroup with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedPersonGroup);
            existedPersonGroup.LastUpdatedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonGroup>().Update(existedPersonGroup);
            _unitOfWork.Save();
        }
        public void DeletePersonGroup(string id)
        {
            var existedPersonGroup = _unitOfWork.GetRepository<PersonGroup>().GetById(id);
            if (existedPersonGroup == null)
            {
                throw new Exception($"PersonGroup with ID {id} doesn't exist!");
            }
            existedPersonGroup.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonGroup>().Update(existedPersonGroup);
            _unitOfWork.Save();
        }
    }
}
