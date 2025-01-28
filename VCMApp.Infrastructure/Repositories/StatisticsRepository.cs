using Microsoft.EntityFrameworkCore;
using VCMApp.Application.Contracts;
using VCMApp.Infrastructure.Persistence;

namespace VCMApp.Infrastructure.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly VCMDbContext _context;

        public StatisticsRepository(VCMDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalVacancies()
        {
            return await _context.Vacancies.CountAsync();
        }

        public async Task<int> GetAppliedApplicants()
        {
            return await _context.ApplicantExamResults.CountAsync();
        }

        public async Task<int> GetFailedApplicants()
        {
            return await _context.ApplicantExamResults.CountAsync(x => x.ResultPercentage <= 49);
        }
    }
}
