using AutoMapper;
using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Domain.Models;

namespace BetterHealthCareAPI.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Procedure, ProcedureDto>().ReverseMap();
            CreateMap<MedicalFile, MedicalFileDto>().ReverseMap();
            CreateMap<PatientAction, PatientActionDto>().ReverseMap();

        }
    }
}
