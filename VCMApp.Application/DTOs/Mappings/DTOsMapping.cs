using AutoMapper;
using VCMApp.Infrastructure.Entities;

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
