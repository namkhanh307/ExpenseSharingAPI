using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.GroupModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Group> GetGroup()
        {
            return _unitOfWork.GetRepository<Group>().Entities.Where(g => !g.DeletedTime.HasValue).ToList();
        }

        public void PostGroup(PostGroupModel model)
        {
            var group = new Group()
            {
                Name = model.Name,
                Size = model.Size,
                Type = model.Type,
                CreatedTime = DateTime.Now,
                LastUpdatedTime = DateTime.Now,
            };
            _unitOfWork.GetRepository<Group>().Insert(group);
            _unitOfWork.Save();
        }

        public void PutGroup(string id, PutGroupModel model)
        {
            var existedGroup = _unitOfWork.GetRepository<Group>().GetById(id);
            if(existedGroup == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            existedGroup.Name = model.Name;
            existedGroup.Size = model.Size;
            existedGroup.Type = model.Type;
            _unitOfWork.GetRepository<Group>().Update(existedGroup);
            _unitOfWork.Save();
        }
        public void DeleteGroup(string id)
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
