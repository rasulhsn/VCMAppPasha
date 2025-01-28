using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.Application.Applicants.Vacancy
{
    public class GetActiveVacancyQuery : IRequest<Result<VacancyDetailDto>>
    {
        public int Id { get; set; }
    }

    public class GetActiveVacancyQueryHandler : IRequestHandler<GetActiveVacancyQuery, Result<VacancyDetailDto>>
    {
        private readonly IVacancyRepository _repository;

        public GetActiveVacancyQueryHandler(IVacancyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<VacancyDetailDto>> Handle(GetActiveVacancyQuery request,
                                                CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetActiveVacancyAsync<VacancyDetailDto>(request.Id);
                return Result.Success<VacancyDetailDto>(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<VacancyDetailDto>(ex.Message);
            }
        }
    }
}
