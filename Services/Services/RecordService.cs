using AutoMapper;
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
        public RecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<GetRecordModel> GetRecord()
        {
            return _mapper.Map<List<GetRecordModel>>(_unitOfWork.GetRepository<Record>().Entities.Where(g => !g.DeletedTime.HasValue).ToList());
        }

        public void PostRecord(PostRecordModel model)
        {
            var record = _mapper.Map<Record>(model);
            record.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Record>().Insert(record);
            _unitOfWork.Save();
        }

        public void PutRecord(string id, PutRecordModel model)
        {
            var existedRecord = _unitOfWork.GetRepository<Record>().GetById(id);
            if (existedRecord == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedRecord);
            existedRecord.LastUpdatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Record>().Update(existedRecord);
            _unitOfWork.Save();
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
