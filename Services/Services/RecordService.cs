using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.RecordModel;
using Services.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class RecordService : IRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string currentUserId => Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

        public RecordService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetRecordModel>> GetRecord(string? recordId, string? reportId)
        {
            List<Record> records = await _unitOfWork.GetRepository<Record>()
                .Entities
                .Where(g => !g.DeletedTime.HasValue &&
                            (reportId == null || g.ReportId == reportId) &&
                            (recordId == null || g.Id == recordId))
                .ToListAsync();

            return _mapper.Map<List<GetRecordModel>>(records);
        }

        public async Task PostRecord(PostRecordModel model)
        {
            var id = Guid.NewGuid().ToString("N");
            string fileName = await FileUploadHelper.UploadFile(model.InvoiceImage, id);

            var newRecord = new Record
            {
                Id = id,
                PersonPayId = model.PersonPayId,
                PersonReceiveId = model.PersonReceiveId,
                InvoiceImage = fileName,
                Amount = model.Amount,
                ReportId = model.ReportId,
                IsPaid = false,
                CreatedTime = DateTime.Now,
                CreatedBy = currentUserId
            };

            await _unitOfWork.GetRepository<Record>().InsertAsync(newRecord);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutRecord(string id, PutRecordModel model)
        {
            var existedRecord = await _unitOfWork.GetRepository<Record>().GetByIdAsync(id);

            if (existedRecord == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Không tìm thấy bản ghi.");
            }

            _mapper.Map(model, existedRecord);

            if (model.InvoiceImage != null)
            {
                string fileName = await FileUploadHelper.UploadFile(model.InvoiceImage, id);
                if (existedRecord.InvoiceImage != null)
                {
                    FileUploadHelper.DeleteFile(existedRecord.InvoiceImage);
                }
                existedRecord.InvoiceImage = fileName;
            }

            if (model.IsPaid.HasValue)
            {
                existedRecord.IsPaid = model.IsPaid.Value;
            }

            await _unitOfWork.GetRepository<Record>().UpdateAsync(existedRecord);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteRecord(string id)
        {
            var existedRecord = await _unitOfWork.GetRepository<Record>().GetByIdAsync(id);

            if (existedRecord == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Không tìm thấy bản ghi.");
            }

            if (existedRecord.InvoiceImage != null)
            {
                FileUploadHelper.DeleteFile(existedRecord.InvoiceImage);
            }

            existedRecord.DeletedTime = DateTime.Now;
            existedRecord.DeletedBy = currentUserId;

            await _unitOfWork.GetRepository<Record>().UpdateAsync(existedRecord);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteRecordFromReport(string reportId)
        {
            List<Record> existedRecords = await _unitOfWork.GetRepository<Record>()
                .Entities
                .Where(r => r.ReportId == reportId && !r.DeletedTime.HasValue)
                .ToListAsync();

            if (!existedRecords.Any())
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Không tìm thấy bản ghi trong báo cáo.");
            }

            existedRecords.ForEach(r =>
            {
                if (r.InvoiceImage != null)
                {
                    FileUploadHelper.DeleteFile(r.InvoiceImage);
                }
                r.DeletedTime = DateTime.Now;
                r.DeletedBy = currentUserId;
            });

            await _unitOfWork.GetRepository<Record>().UpdateRangeAsync(existedRecords);
            await _unitOfWork.SaveAsync();
        }
    }
}
