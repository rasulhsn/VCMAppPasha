
namespace VCMApp.LightDomain.Entities
{
    public class Applicant
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public ICollection<ApplicantCV> ApplicantCVs { get; set; } = new List<ApplicantCV>();
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
