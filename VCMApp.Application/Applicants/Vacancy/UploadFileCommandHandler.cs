using CSharpFunctionalExtensions;
using MediatR;
using VCMApp.Infrastructure.Repositories.Abstract;

namespace VCMApp.Application.Applicants.Vacancy
{
    public class UploadFileCommand : IRequest<Result>
    {
        public Guid SessionId { get; set; }
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Result>
    {
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
        private readonly string[] AllowedTypes = { "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" };

        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicantRepository _applicantRepository;

        public UploadFileCommandHandler(IApplicationRepository applicationRepository,
                                        IApplicantRepository applicantRepository)
        {
            _applicationRepository = applicationRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task<Result> Handle(UploadFileCommand request,
                                                CancellationToken cancellationToken)
        {
            if (request == null || request.FileContent == null)
            {
                Result.Failure("Please select a file to upload.");
            }
            if (!AllowedTypes.Contains(request.ContentType))
            {
                Result.Failure("Invalid file type. Only PDF and DOCX files are allowed.");
            }
            if (request.FileContent.Length > MaxFileSize)
            {
                Result.Failure("File size exceeds the 5 MB limit.");
            }

            var application = await _applicationRepository.GetBySessionGuid(request.SessionId);

            var filePath = Path.Combine("Uploads", $"{Guid.NewGuid()}_{request.FileName}");
            await File.WriteAllBytesAsync(filePath, request.FileContent, cancellationToken);

            await _applicantRepository.AddApplicantCv(new Infrastructure.Entities.ApplicantCV()
            {
                ApplicantId = application.ApplicantId,
                UploadDate = DateTime.UtcNow,
                FileExtension = request.ContentType,
                FilePath = filePath
            });
            await _applicantRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
