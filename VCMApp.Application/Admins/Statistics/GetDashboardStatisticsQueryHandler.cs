using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.Application.Admins.Statistics
{
    public class GetDashboardStatisticsQuery : IRequest<Result<DashboardStatisticsDto>>
    {
    }

    public class GetDashboardStatisticsQueryHandler : IRequestHandler<GetDashboardStatisticsQuery, Result<DashboardStatisticsDto>>
    {
        private readonly IStatisticsRepository _repository;

        public GetDashboardStatisticsQueryHandler(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<DashboardStatisticsDto>> Handle(GetDashboardStatisticsQuery request,
                        CancellationToken cancellationToken)
        {
            try
            {
                var totalVacancies = await _repository.GetTotalVacancies();
                var appliedApplicants = await _repository.GetAppliedApplicants();
                var failedApplicants = await _repository.GetFailedApplicants();

                return Result.Success(new DashboardStatisticsDto
                {
                    TotalVacancies = totalVacancies,
                    AppliedApplicants = appliedApplicants,
                    FailedApplicants = failedApplicants
                });
            }
            catch (Exception ex)
            {
                return Result.Failure<DashboardStatisticsDto>(ex.Message);
            }
        }
    }
}
