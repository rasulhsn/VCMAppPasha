using Microsoft.EntityFrameworkCore;
using VCMApp.LightDomain.Entities;
using VCMApp.Infrastructure.Persistence;
using VCMApp.Application.Contracts;

namespace VCMApp.Infrastructure.Repositories
{
    public class ApplicantRepository : Repository<Applicant>, IApplicantRepository
    {
        private readonly VCMDbContext _context;

        public ApplicantRepository(VCMDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddApplicantCv(ApplicantCV cv)
        {
            await _context.ApplicantCVs.AddAsync(cv);
        }

        public async Task AddApplicantExamResult(ApplicantExamResult result)
        {
            await _context.ApplicantExamResults.AddAsync(result);
        }

        public async Task AddApplicantAnswer(ApplicantExamAnswer applicantExamAnswer)
        {
            await _context.ApplicantExamAnswers.AddAsync(applicantExamAnswer);
        }

        public async Task<ApplicantExamAnswer> GetApplicantAnswer(int applicationId, int questionId)
        {
            return await _context.ApplicantExamAnswers
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.ApplicationId == applicationId
                    && x.ExamQuestionId == questionId
                    && x.SelectedExamQuestionOptionId == null);
        }

        public void UpdateApplicantAnswer(ApplicantExamAnswer applicantExamAnswer)
        {
            _context.ApplicantExamAnswers.Update(applicantExamAnswer);
        }

        public async Task<int> GetApplicantAnswersCount(int applicationId)
        {
            return await _context.ApplicantExamAnswers
                        .Where(x => x.ApplicationId == applicationId)
                        .CountAsync();
        }

        public async Task<List<ApplicantExamAnswer>> GetApplicantAnswers(int applicationId)
        {
            return await _context.ApplicantExamAnswers
                        .Where(x => x.ApplicationId == applicationId)
                        .ToListAsync();
        }

        public async Task<Applicant?> GetByEmailAsync(string email)
        {
            return await _context.Applicants.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
