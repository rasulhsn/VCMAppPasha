using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs;
using VCMApp.LightDomain.Entities;

namespace VCMApp.Application.Applicants.Vacancy
{
    public class ApplyApplicantCommand : IRequest<Result<ApplyApplicantDto>>
    {
        public int VacancyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class ApplyApplicantCommandHandler : IRequestHandler<ApplyApplicantCommand, Result<ApplyApplicantDto>>
    {
        private readonly IApplicantRepository _repository;

        public ApplyApplicantCommandHandler(IApplicantRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ApplyApplicantDto>> Handle(ApplyApplicantCommand request,
                                                CancellationToken cancellationToken)
        {
            // needs to add validation
            var applicantEntity = new Applicant()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };

            await _repository.AddAsync(applicantEntity);
            await _repository.SaveChangesAsync();

            return Result.Success<ApplyApplicantDto>(new ApplyApplicantDto()
            {
                ApplicantId = applicantEntity.Id,
                FullName = $"{applicantEntity.FirstName} {applicantEntity.LastName}"
            });
        }
    }
}
