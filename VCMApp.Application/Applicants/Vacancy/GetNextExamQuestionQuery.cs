using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs;
using VCMApp.LightDomain.DTOs.ErrorTypes;
using VCMApp.LightDomain.Entities;

namespace VCMApp.Application.Applicants.Vacancy
{
    public class GetNextExamQuestionQuery : IRequest<Result<ExamQuestionDto, ExamErrorType>>
    {
        public int QuestionIndex { get; set; }
        public Guid SessionId { get; set; }
    }

    public class GetNextExamQuestionQueryHandler : IRequestHandler<GetNextExamQuestionQuery, Result<ExamQuestionDto, ExamErrorType>>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicantRepository _applicantRepository;

        public GetNextExamQuestionQueryHandler(IVacancyRepository vacancyRepository,
                                               IApplicationRepository applicationRepository,
                                               IApplicantRepository applicantRepository)
        {
            _vacancyRepository = vacancyRepository;
            _applicationRepository = applicationRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task<Result<ExamQuestionDto,ExamErrorType>> Handle(GetNextExamQuestionQuery request,
                                                CancellationToken cancellationToken)
        {
            try
            {
                var sessionApp = await _applicationRepository.GetBySessionGuid(request.SessionId);

                if (sessionApp == null || !sessionApp.IsActive 
                        || sessionApp.EndDate <= DateTime.UtcNow)
                {
                    return Result.Failure<ExamQuestionDto,ExamErrorType>(ExamErrorType.ExamIsExpired);
                }

                int questionCount = await _applicantRepository.GetApplicantAnswersCount(sessionApp.Id);

                int remainingQuestionCount = sessionApp.ExamQuestionCount - questionCount;

                if (remainingQuestionCount == 0)
                {
                    return Result.Failure<ExamQuestionDto, ExamErrorType>(ExamErrorType.ExamFinished);
                }

                dynamic dynamicQuestion = await _vacancyRepository.GetNextQuestionAsync(sessionApp.VacancyId);

                List<QuestionOption> questionOptions = new List<QuestionOption>();

                foreach (dynamic item in dynamicQuestion.Options)
                {
                    questionOptions.Add(new QuestionOption
                    {
                        Content = item.Content,
                        IsCorrect = item.IsCorrect,
                        PriorityNumber = item.PriorityNumber,
                        QuestionId = item.QuestionId,
                        QuestionOptionId = item.QuestionOptionId,
                    });
                }

                ExamQuestionDto examQuestionDto = new ExamQuestionDto
                {
                    QuestionId = dynamicQuestion.QuestionId,
                    QuestionContent = dynamicQuestion.QuestionContent,
                    Options = questionOptions
                };

                await _applicantRepository.AddApplicantAnswer(new ApplicantExamAnswer()
                {
                    ApplicationId = sessionApp.Id,
                    ExamQuestionId = examQuestionDto.QuestionId,
                });
                await _applicantRepository.SaveChangesAsync();

                return Result.Success<ExamQuestionDto, ExamErrorType>(examQuestionDto);
            }
            catch (Exception ex)
            {
                return Result.Failure<ExamQuestionDto, ExamErrorType>(ExamErrorType.OccuredError);
            }
        }
    }
}
