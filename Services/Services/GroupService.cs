using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.GroupModel;
using Services.IServices;

namespace Services.Services
{
    public class GroupService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private string currentUserId => Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

        public async Task<List<GetGroupModel>> GetGroups()
        {
            return _mapper.Map<List<GetGroupModel>>(await _unitOfWork.GetRepository<Group>().Entities.Where(g => !g.DeletedTime.HasValue).ToListAsync());        
        }
        public async Task PostGroup(PostGroupModel model)
        {
            var group = _mapper.Map<Group>(model);
            group.CreatedTime = DateTime.Now;
            group.CreatedBy = currentUserId;
            await _unitOfWork.GetRepository<Group>().InsertAsync(group);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutGroup(string id, PutGroupModel model)
        {
            string? fileName = await FileUploadHelper.UploadFile(model.Wallpaper, id);

            var existedGroup = await _unitOfWork.GetRepository<Group>().GetByIdAsync(id);

            if (existedGroup == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                if (existedGroup.Wallpaper != null)
                {
                    FileUploadHelper.DeleteFile(existedGroup.Wallpaper);
                }
                existedGroup.Wallpaper = fileName;
            }

            existedGroup.LastUpdatedTime = DateTime.Now;
            existedGroup.LastUpdatedBy = currentUserId;
            await _unitOfWork.GetRepository<Group>().UpdateAsync(existedGroup);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteGroup(string id)
        {
            var existedGroup = await _unitOfWork.GetRepository<Group>().GetByIdAsync(id);
            if (existedGroup == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            existedGroup.DeletedTime = DateTime.Now;
            existedGroup.DeletedBy = currentUserId;
            await _unitOfWork.GetRepository<Group>().UpdateAsync(existedGroup);
            await _unitOfWork.SaveAsync();
        }
    }
}
