using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string currentUserId => Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetPersonModel>> GetPersons(string? id)
        {
            List<Person> response = await _unitOfWork.GetRepository<Person>()
                .Entities.Where(g => !g.DeletedTime.HasValue)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(id))
            {
                response = response.Where(p => p.Id == id).ToList();
            }

            return _mapper.Map<List<GetPersonModel>>(response);
        }

        public async Task PostPerson(PostPersonModel model)
        {
            Person? person = await _unitOfWork.GetRepository<Person>()
                .Entities.FirstOrDefaultAsync(p => p.Phone == model.Phone);

            if (person != null)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ErrorCode.Conflicted, "Số điện thoại này đã được sử dụng.");
            }

            person = _mapper.Map<Person>(model);

            person.CreatedTime = DateTime.Now;
            person.CreatedBy = currentUserId;

            await _unitOfWork.GetRepository<Person>().InsertAsync(person);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutPerson(string id, PutPersonModel model)
        {
            Person existedPerson = await _unitOfWork.GetRepository<Person>().GetByIdAsync(id);
            if (existedPerson == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Người dùng không tồn tại.");
            }

            _mapper.Map(model, existedPerson);

            existedPerson.LastUpdatedTime = DateTime.Now;
            existedPerson.LastUpdatedBy = currentUserId;

            await _unitOfWork.GetRepository<Person>().UpdateAsync(existedPerson);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePerson(string id)
        {
            Person existedPerson = await _unitOfWork.GetRepository<Person>().GetByIdAsync(id);

            if (existedPerson == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Người dùng không tồn tại.");
            }

            existedPerson.DeletedBy = currentUserId;
            existedPerson.DeletedTime = DateTime.Now;

            await _unitOfWork.GetRepository<Person>().UpdateAsync(existedPerson);
            await _unitOfWork.SaveAsync();
        }
    }
}
