using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.Application.Applicants.Vacancy
{
    public class GetExamDetailQueryQuery : IRequest<Result<VacancyExamDetailDto>>
    {
        public int VacancyId { get; set; }
    }

    public class GetExamDetailQueryHandler : IRequestHandler<GetExamDetailQueryQuery, Result<VacancyExamDetailDto>>
    {
        private readonly IVacancyRepository _repository;

        public GetExamDetailQueryHandler(IVacancyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<VacancyExamDetailDto>> Handle(GetExamDetailQueryQuery request,
                                                        CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetVacancyExamDetailByVacancyId<VacancyExamDetailDto>(request.VacancyId);
                return Result.Success<VacancyExamDetailDto>(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<VacancyExamDetailDto>(ex.Message);
            }
        }
    }
}
