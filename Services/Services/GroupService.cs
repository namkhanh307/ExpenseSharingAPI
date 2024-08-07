using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.GroupModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private string currentUserId => Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

        public async Task<List<GetGroupModel>> GetGroups()
        {
            List<Group> groups = await _unitOfWork.GetRepository<Group>().Entities
                .Where(g => !g.DeletedTime.HasValue)
                .ToListAsync();
            return _mapper.Map<List<GetGroupModel>>(groups);
        }

        public async Task PostGroup(PostGroupModel model)
        {
            Group? existingGroup = await _unitOfWork.GetRepository<Group>()
                .Entities.FirstOrDefaultAsync(g => g.Name == model.Name);

            if (existingGroup != null)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.Conflicted, "Nhóm này đã tồn tại!");
            }

            Group group = _mapper.Map<Group>(model);
            group.CreatedTime = DateTime.Now;
            group.CreatedBy = currentUserId;

            await _unitOfWork.GetRepository<Group>().InsertAsync(group);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutGroup(string id, PutGroupModel model)
        {
            Group? existedGroup = await _unitOfWork.GetRepository<Group>().GetByIdAsync(id);

            if (existedGroup == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Nhóm không tồn tại!");
            }

            _mapper.Map(model, existedGroup);

            if (model.Wallpaper != null)
            {
                string? fileName = await FileUploadHelper.UploadFile(model.Wallpaper, id);
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
            Group? existedGroup = await _unitOfWork.GetRepository<Group>().GetByIdAsync(id);

            if (existedGroup == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Nhóm không tồn tại!");
            }

            existedGroup.DeletedTime = DateTime.Now;
            existedGroup.DeletedBy = currentUserId;

            await _unitOfWork.GetRepository<Group>().UpdateAsync(existedGroup);
            await _unitOfWork.SaveAsync();
        }
    }
}
