using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.RecordModel;
using Services.IServices;

namespace Services.Services
{
    public class RecordService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IRecordService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<List<GetRecordModel>> GetRecord(string? recordId, string? reportId)
        {
            List<Record> records = await _unitOfWork.GetRepository<Record>().Entities
                        .Where(g => !g.DeletedTime.HasValue && (reportId == null || g.ReportId == reportId) && (recordId == null || g.Id == recordId))
                        .ToListAsync();
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
                PersonPayId = model.PersonPayId,
                PersonReceiveId = model.PersonReceiveId,
                InvoiceImage = fileName,
                Amount = model.Amount,
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
                if (existedRecord.InvoiceImage != null)
                {
                    FileUploadHelper.DeleteFile(existedRecord.InvoiceImage);
                }
                existedRecord.InvoiceImage = fileName;
            }

            existedRecord.IsPaid = model.IsPaid;

            await _unitOfWork.GetRepository<Record>().UpdateAsync(existedRecord);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteRecord(string id)
        {
            var existedRecord = _unitOfWork.GetRepository<Record>().GetById(id);
            if (existedRecord == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            if (existedRecord.InvoiceImage != null)
            {
                FileUploadHelper.DeleteFile(existedRecord.InvoiceImage);
            }
            existedRecord.DeletedTime = DateTime.Now;
            await _unitOfWork.GetRepository<Record>().UpdateAsync(existedRecord);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteRecordFromReport(string reportId)
        {
            var existedRecord = _unitOfWork.GetRepository<Record>().Entities.Where(r => r.ReportId == reportId && !r.DeletedTime.HasValue).ToList();
            //await FileUploadHelper.DeleteFile(existedRecord.InvoiceImage);
            existedRecord.ForEach(r => { r.DeletedTime = DateTime.Now;});
            await _unitOfWork.GetRepository<Record>().UpdateRangeAsync(existedRecord);
            await _unitOfWork.SaveAsync();
        }

    }
}
