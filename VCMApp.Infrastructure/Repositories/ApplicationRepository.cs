using Microsoft.EntityFrameworkCore;
using VCMApp.Application.Contracts;
using VCMApp.Infrastructure.Persistence;
using VCMApp.LightDomain.Entities;

namespace VCMApp.Infrastructure.Repositories
{
    public class ApplicationRepository : Repository<LightDomain.Entities.Application>, IApplicationRepository
    {
        private readonly VCMDbContext _context;

        public ApplicationRepository(VCMDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LightDomain.Entities.Application?> GetBySessionGuid(Guid sessionGuid)
        {
            return await _context.Applications
                    .FirstOrDefaultAsync(x => x.SessionGuid == sessionGuid);
        }

        public async Task<List<ApplicationExamResultRaw>> GetExamResults()
        {
            string query = @"SELECT AER.[Id] AS ApplicantExamResultId
                    ,AER.[ApplicationId] AS ApplicationId
                    ,AER.[CorrectAnswersCount] AS CorrectAnswersCount
                    ,AER.[IncorrectAnswersCount] AS InCorrectAnswersCount
                    ,AER.[ResultPercentage] AS ResultPercentage
	                ,APC.Id AS ApplicantId
	                ,APC.FirstName
	                ,APC.LastName
	                ,APC.Email
		            ,VC.Name AS VacancyName
                    FROM [dbo].[ApplicantExamResults] AS AER
                    INNER JOIN [dbo].[Applications] AS APP ON APP.Id = AER.ApplicationId
                    INNER JOIN [dbo].[Applicants] AS APC ON APC.Id = APP.ApplicantId
		            INNER JOIN [dbo].[Vacancies] AS VC ON VC.Id = APP.VacancyId";


            return await _context.Set<ApplicationExamResultRaw>()
                        .FromSqlRaw(query)
                        .ToListAsync();
        }

        public async Task<List<QuestionAnswerDetailRaw>> GetQuestionAnswerDetail(int applicationId)
        {
            string query = @"SELECT EQ.QuestionContent, EQO.Content AS AnswerContent,
            (SELECT IsCorrect FROM dbo.ExamQuestionOptions
            WHERE ExamQuestionId = AEA.ExamQuestionId AND Id = AEA.SelectedExamQuestionOptionId) AS AnswerIsCorrect
            FROM dbo.Applications AS AP 
            INNER JOIN dbo.ApplicantExamAnswers AS AEA ON AEA.ApplicationId = AP.Id
            INNER JOIN dbo.ExamQuestions AS EQ ON EQ.Id = AEA.ExamQuestionId
            INNER JOIN dbo.ExamQuestionOptions AS EQO ON EQO.Id = AEA.SelectedExamQuestionOptionId
            WHERE AP.Id = {0} AND AEA.SelectedExamQuestionOptionId IS NOT NULL";


            return await _context.Set<QuestionAnswerDetailRaw>()
                        .FromSqlRaw(query, applicationId)
                        .ToListAsync();
        }
    }
}
