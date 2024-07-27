using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.RecordModel;
using Services.IServices;

namespace Services.Services
{
    public class RecordService : IRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RecordService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<GetRecordModel> GetRecord(string? recordId, string? reportId)
        {
            var records = _unitOfWork.GetRepository<Record>().Entities
                        .Where(g => !g.DeletedTime.HasValue && (reportId == null || g.ReportId == reportId) && (recordId == null || g.Id == recordId))
                        .ToList();
            return _mapper.Map<List<GetRecordModel>>(records);
        }

        public async Task PostRecord(PostRecordModel model)
        {
            string idUser = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var id = Guid.NewGuid().ToString("N");
            string fileName = await FileUploadHelper.UploadFile(model.InvoiceImage, id);
            var newRecord = new Record()
            {
                Id = id,
                ExpenseId = model.ExpenseId,
                PersonId = model.PersonId,
                InvoiceImage = fileName,
                ReportId = model.ReportId,
                IsPaid = false,
                CreatedTime = DateTime.Now,
                CreatedBy = idUser
            };
            await _unitOfWork.GetRepository<Record>().InsertAsync(newRecord);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutRecord(string id, PutRecordModel model)
        {
            string fileName = await FileUploadHelper.UploadFile(model.InvoiceImage, id);
            
            var existedRecord = _unitOfWork.GetRepository<Record>().GetById(id);
            if (existedRecord == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }

            if (fileName != null)
            {
                existedRecord.InvoiceImage = fileName;
            }

            existedRecord.IsPaid = model.IsPaid;

            await _unitOfWork.GetRepository<Record>().UpdateAsync(existedRecord);
            await _unitOfWork.SaveAsync();
        }
        public void DeleteRecord(string id)
        {
            var existedRecord = _unitOfWork.GetRepository<Record>().GetById(id);
            if (existedRecord == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            existedRecord.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<Record>().Update(existedRecord);
            _unitOfWork.Save();
        }
    }
}
