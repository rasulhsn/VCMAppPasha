using VCMApp.LightDomain.Entities;

namespace VCMApp.Application.Contracts
{
    public interface IVacancyRepository : IRepository<Vacancy>
    {
        Task<Vacancy> GetVacancyWithDetailsAsync(int id);
        Task<List<TResult>> GetActiveVacanciesAsync<TResult>();
        Task<TResult> GetActiveVacancyAsync<TResult>(int id);
        Task<dynamic> GetNextQuestionAsync(int vacancyId);
        Task<VacancyExamDetail> GetVacancyExamDetailByVacancyId(int vacancyId);
        Task<TResult> GetVacancyExamDetailByVacancyId<TResult>(int vacancyId);
    }
}
