using AutoMapper;
using Repositories.Entities;
using Repositories.IRepositories;
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

        public void DeleteRecord(string id)
        {
            throw new NotImplementedException();
        }

        public List<Record> GetRecord()
        {
            throw new NotImplementedException();
        }

        public void PostRecord(PostRecordModel model)
        {
            throw new NotImplementedException();
        }

        public void PutRecord(string id, PutRecordModel model)
        {
            throw new NotImplementedException();
        }
    }
}
