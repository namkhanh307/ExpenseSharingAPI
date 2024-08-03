using AutoMapper;
using Repositories.Entities;
using Repositories.ResponseModel.RecordModel;

namespace Services.Mapper
{
    public class RecordProfile : Profile
    {
        public RecordProfile()
        {
            CreateMap<Record, GetRecordModel>().ReverseMap();
            CreateMap<Record, PostRecordModel>().ReverseMap();
            CreateMap<Record, PutRecordModel>().ReverseMap();
        }

    }
}
