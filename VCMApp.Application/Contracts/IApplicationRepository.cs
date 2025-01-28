
namespace VCMApp.Application.Contracts
{
    public interface IApplicationRepository : IRepository<LightDomain.Entities.Application>
    {
        Task<LightDomain.Entities.Application?> GetBySessionGuid(Guid sessionGuid);
        Task<List<LightDomain.Entities.ApplicationExamResultRaw>> GetExamResults();
        Task<List<LightDomain.Entities.QuestionAnswerDetailRaw>> GetQuestionAnswerDetail(int applicationId);
    }
}
