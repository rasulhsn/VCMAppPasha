using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.Entities;

namespace VCMApp.Application.Applicants.Vacancy
{
    public class CloseApplicationCommand : IRequest<Result>
    {
        public Guid ApplicationSessionGuid { get; set; }
    }

    public class CloseApplicationCommandHandler : IRequestHandler<CloseApplicationCommand, Result>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IExamQuestionRepository _examQuestionRepository;

        public CloseApplicationCommandHandler(IApplicationRepository applicationRepository,
                            IApplicantRepository applicantRepository,
                            IExamQuestionRepository examQuestionRepository)
        {
            _applicationRepository = applicationRepository;
            _applicantRepository = applicantRepository;
            _examQuestionRepository = examQuestionRepository;
        }

        public async Task<Result> Handle(CloseApplicationCommand request,
                                                CancellationToken cancellationToken)
        {
            var sessionApp = await _applicationRepository.GetBySessionGuid(request.ApplicationSessionGuid);

            if (!sessionApp.IsActive)
                return Result.Success();

            sessionApp.IsActive = false;
            _applicationRepository.Update(sessionApp);
            await _applicationRepository.SaveChangesAsync();

            // calculate answers
            // and write to db as result.
            var answers = await _applicantRepository.GetApplicantAnswers(sessionApp.Id);

            ApplicantExamResult result = new ApplicantExamResult();
            result.ApplicationId = sessionApp.Id;

            foreach (var item in answers)
            {
                bool answerIsCorrect = false;
                
                if (item.SelectedExamQuestionOptionId.HasValue)
                {
                    answerIsCorrect = await _examQuestionRepository.IsQuestionAnswerCorrect(item.ExamQuestionId, item.SelectedExamQuestionOptionId.Value);
                }

                if (answerIsCorrect)
                    result.CorrectAnswersCount += 1;
            }

            if (result.CorrectAnswersCount >= 1 && result.CorrectAnswersCount != sessionApp.ExamQuestionCount)
                result.IncorrectAnswersCount = (short)(sessionApp.ExamQuestionCount - result.CorrectAnswersCount);
            else if (result.CorrectAnswersCount == sessionApp.ExamQuestionCount)
                result.IncorrectAnswersCount = 0;
            else 
                result.IncorrectAnswersCount = (short)sessionApp.ExamQuestionCount;

            result.ResultPercentage = CalculateResultPercentage(result.CorrectAnswersCount, result.IncorrectAnswersCount);

            await _applicantRepository.AddApplicantExamResult(result);
            await _applicantRepository.SaveChangesAsync();

            return Result.Success();
        }

        private short CalculateResultPercentage(short correctAnswersCount, short incorrectAnswersCount)
        {
            short totalQuestions = (short)(correctAnswersCount + incorrectAnswersCount);

            if (totalQuestions == 0)
            {
                return 0;
            }

            // Explicitly cast the result back to short after calculation
            return (short)((correctAnswersCount * 100) / totalQuestions);
        }
    }
}
