using VCMApp.LightDomain.Entities;

namespace VCMApp.Application.Contracts
{
    public interface IExamQuestionRepository : IRepository<ExamQuestion>
    {
        Task<bool> IsQuestionAnswerCorrect(int questionId, int optionId);
    }
}
