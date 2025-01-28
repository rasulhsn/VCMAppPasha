using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs;


namespace VCMApp.Application.Applicants.Vacancy
{
    public class GetActiveVacanciesQuery : IRequest<Result<List<VacancyDto>>>
    {
    }

    public class GetActiveVacanciesQueryHandler : IRequestHandler<GetActiveVacanciesQuery, Result<List<VacancyDto>>>
    {
        private readonly IVacancyRepository _repository;

        public GetActiveVacanciesQueryHandler(IVacancyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<VacancyDto>>> Handle(GetActiveVacanciesQuery request,
                        CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetActiveVacanciesAsync<VacancyDto>();
                return Result.Success<List<VacancyDto>>(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<VacancyDto>>(ex.Message);
            }
        }
    }
}
