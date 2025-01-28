using AutoMapper;
using VCMApp.LightDomain.DTOs;
using VCMApp.LightDomain.Entities;

namespace VCMApp.Application.DTOs.Mappings
{
    public class DTOsMapping : Profile
    {
        public DTOsMapping()
        {
            CreateMap<Vacancy, VacancyDto>();

            CreateMap<Vacancy, VacancyDetailDto>();

            CreateMap<VacancyExamDetail, VacancyExamDetailDto>();

            CreateMap<VacancyExamQuestion, ExamQuestionDto>();

            CreateMap<ExamQuestion, ExamQuestionDto>();

            CreateMap<ApplicationExamResultRaw, ExamResultDto>();

            CreateMap<QuestionAnswerDetailRaw, QuestionAnswerDto>();
        }
    }
}
