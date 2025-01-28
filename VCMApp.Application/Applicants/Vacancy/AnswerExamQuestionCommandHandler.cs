using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;

namespace VCMApp.Application.Applicants.Vacancy
{
    public class AnswerExamQuestionCommand : IRequest<Result>
    {
        public Guid ApplicationSessionGuid { get; set; }
        public int QuestionId { get; set; }
        public int AnswerOptionId { get; set; }
    }

    public class AnswerExamQuestionCommandHandler : IRequestHandler<AnswerExamQuestionCommand, Result>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IApplicationRepository _applicationRepository;

        public AnswerExamQuestionCommandHandler(IApplicantRepository applicantRepository,
                                                IApplicationRepository applicationRepository)
        {
            _applicantRepository = applicantRepository;
            _applicationRepository = applicationRepository;
        }

        public async Task<Result> Handle(AnswerExamQuestionCommand request,
                                                CancellationToken cancellationToken)
        {
            try
            {
                var sessionApp = await _applicationRepository.GetBySessionGuid(request.ApplicationSessionGuid);

                if (sessionApp == null || !sessionApp.IsActive || sessionApp.EndDate <= DateTime.UtcNow)
                    return Result.Failure("Exam session is no longer active.");

                var unansweredQuestion = await _applicantRepository.GetApplicantAnswer(sessionApp.Id, request.QuestionId);

                if (unansweredQuestion == null)
                    return Result.Failure("Question/Answer not exists!");

                unansweredQuestion.SelectedExamQuestionOptionId = request.AnswerOptionId;

                _applicantRepository.UpdateApplicantAnswer(unansweredQuestion);
                await _applicantRepository.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
