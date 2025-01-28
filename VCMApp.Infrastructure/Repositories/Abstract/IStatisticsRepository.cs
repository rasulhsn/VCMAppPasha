
namespace VCMApp.Infrastructure.Repositories.Abstract
{
    public interface IStatisticsRepository
    {
        Task<int> GetTotalVacancies();
        Task<int> GetAppliedApplicants();
        Task<int> GetFailedApplicants();
    }
}
