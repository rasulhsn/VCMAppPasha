using VCMApp.Infrastructure.Entities;

namespace VCMApp.Infrastructure.Repositories.Abstract
{
    public interface IExamQuestionRepository : IRepository<ExamQuestion>
    {
        Task<bool> IsQuestionAnswerCorrect(int questionId, int optionId);
    }
}
