using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.Application.Admins.Exams
{
    public class GetExamAnswersQuery : IRequest<Result<List<QuestionAnswerDto>>>
    {
        public int ApplicationId { get; set; }
    }

    public class GetExamAnswersQueryHandler : IRequestHandler<GetExamAnswersQuery, Result<List<QuestionAnswerDto>>>
    {
        private readonly IApplicationRepository _repository;
        private readonly IMapper _mapper;

        public GetExamAnswersQueryHandler(IApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<QuestionAnswerDto>>> Handle(GetExamAnswersQuery request,
                        CancellationToken cancellationToken)
        {
            try
            {
                var results = await _repository.GetQuestionAnswerDetail(request.ApplicationId);
                var mappedInstance = _mapper.Map<List<QuestionAnswerDto>>(results);
                return Result.Success(mappedInstance);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<QuestionAnswerDto>>(ex.Message);
            }
        }
    }
}
