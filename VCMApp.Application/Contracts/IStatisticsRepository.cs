
namespace VCMApp.Application.Contracts
{
    public interface IStatisticsRepository
    {
        Task<int> GetTotalVacancies();
        Task<int> GetAppliedApplicants();
        Task<int> GetFailedApplicants();
    }
}
