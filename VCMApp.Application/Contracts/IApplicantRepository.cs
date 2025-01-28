using VCMApp.LightDomain.Entities;

namespace VCMApp.Application.Contracts
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
        Task<Applicant?> GetByEmailAsync(string email);
        Task<int> GetApplicantAnswersCount(int applicationId);
        Task AddApplicantAnswer(ApplicantExamAnswer applicantExamAnswer);
        Task<ApplicantExamAnswer> GetApplicantAnswer(int applicationId, int questionId);
        void UpdateApplicantAnswer(ApplicantExamAnswer applicantExamAnswer);
        Task<List<ApplicantExamAnswer>> GetApplicantAnswers(int applicationId);
        Task AddApplicantExamResult(ApplicantExamResult result);
        Task AddApplicantCv(ApplicantCV cv);
    }
}
