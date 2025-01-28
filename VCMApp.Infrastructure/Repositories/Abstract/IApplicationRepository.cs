using VCMApp.Infrastructure.Entities;

namespace VCMApp.Infrastructure.Repositories.Abstract
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Task<Application?> GetBySessionGuid(Guid sessionGuid);
        Task<List<ApplicationExamResultRaw>> GetExamResults();
        Task<List<QuestionAnswerDetailRaw>> GetQuestionAnswerDetail(int applicationId);
    }
}
