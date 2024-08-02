using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.GroupModel;
using Services.IServices;

namespace Services.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetGroupModel>> GetGroups()
        {
            return _mapper.Map<List<GetGroupModel>>(await _unitOfWork.GetRepository<Group>().Entities.Where(g => !g.DeletedTime.HasValue).ToListAsync());        
        }

        public async Task PostGroup(PostGroupModel model)
        {
            var group = _mapper.Map<Group>(model);
            group.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Group>().Insert(group);
            _unitOfWork.Save();
        }

        public async Task PutGroup(string id, PutGroupModel model)
        {
            var existedGroup = _unitOfWork.GetRepository<Group>().GetById(id);
            if (existedGroup == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedGroup);
            existedGroup.LastUpdatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Group>().Update(existedGroup);
            _unitOfWork.Save();
        }
        public async Task DeleteGroup(string id)
        {
            var existedGroup = _unitOfWork.GetRepository<Group>().GetById(id);
            if (existedGroup == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            existedGroup.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<Group>().Update(existedGroup);
            _unitOfWork.Save();
        }
    }
}
