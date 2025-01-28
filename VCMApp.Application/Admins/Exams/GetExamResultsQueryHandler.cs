using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.Application.Admins.Statistics
{
    public class GetExamResultsQuery : IRequest<Result<List<ExamResultDto>>>
    {
        
    }

    public class GetExamResultsQueryHandler : IRequestHandler<GetExamResultsQuery, Result<List<ExamResultDto>>>
    {
        private readonly IApplicationRepository _repository;
        private readonly IMapper _mapper;

        public GetExamResultsQueryHandler(IApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<ExamResultDto>>> Handle(GetExamResultsQuery request,
                        CancellationToken cancellationToken)
        {
            try
            {
                var results = await _repository.GetExamResults();
                return Result.Success(_mapper.Map<List<ExamResultDto>>(results));
            }
            catch (Exception ex)
            {
                return Result.Failure<List<ExamResultDto>>(ex.Message);
            }
        }
    }
}
