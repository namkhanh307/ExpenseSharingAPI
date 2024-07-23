using AutoMapper;
using Repositories.Entities;
using Repositories.ResponseModel.ReportModel;

namespace Services.Mapper
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, GetReportModel>().ReverseMap();
            CreateMap<Report, PutReportModel>().ReverseMap();
            CreateMap<Report, PostReportModel>().ReverseMap();
        }

    }
}
