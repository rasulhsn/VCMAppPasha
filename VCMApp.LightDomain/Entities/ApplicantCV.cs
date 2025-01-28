
namespace VCMApp.LightDomain.Entities
{
    public class ApplicantCV
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string FilePath { get; set; } = null!;
        public string? FileExtension { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;

        public Applicant Applicant { get; set; } = null!;
    }
}
