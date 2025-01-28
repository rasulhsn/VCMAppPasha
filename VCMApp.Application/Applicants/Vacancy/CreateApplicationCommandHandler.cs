using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.Application.Applicants.Vacancy
{
    public class CreateApplicationCommand : IRequest<Result<CreatedApplicationDto>>
    {
        public int VacancyId { get; set; }
        public int ApplicantId { get; set; }
    }

    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, Result<CreatedApplicationDto>>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IApplicationRepository _applicationRepository;

        public CreateApplicationCommandHandler(IVacancyRepository vacancyRepository,
                                                   IApplicationRepository applicationRepository)
        {
            _vacancyRepository = vacancyRepository;
            _applicationRepository = applicationRepository;
        }

        public async Task<Result<CreatedApplicationDto>> Handle(CreateApplicationCommand request,
                                                CancellationToken cancellationToken)
        {
            var vacancyExamDetail = await _vacancyRepository.GetVacancyExamDetailByVacancyId(request.VacancyId);

            var applicationEntity = new LightDomain.Entities.Application()
            {
                VacancyId = request.VacancyId,
                ApplicantId = request.ApplicantId,
                IsActive = true,
                SessionGuid = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMinutes(vacancyExamDetail.TotalTimeOfTestInMinute),
                ExamQuestionCount = vacancyExamDetail.ExamQuestionCount,
            };

            await _applicationRepository.AddAsync(applicationEntity);
            await _applicationRepository.SaveChangesAsync();

            return Result.Success<CreatedApplicationDto>(new CreatedApplicationDto()
            {
                ApplicationSessinGuid = applicationEntity.SessionGuid,
                ExamTotalTimeInMinute = vacancyExamDetail.TotalTimeOfTestInMinute
            });
        }
    }
}
